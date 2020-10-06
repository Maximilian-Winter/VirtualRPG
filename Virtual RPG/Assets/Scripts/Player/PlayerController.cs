using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ApplicationStates applicationStates;

    [SerializeField]
    private GameEvent playerDeadEvent;

    [SerializeField]
    private GameEvent playerUseActiveItem;

    [SerializeField]
    private GameEvent showInventory;

    [SerializeField]
    private GameEvent nextItemBarSlot;

    [SerializeField]
    private GameEvent lastItemBarSlot;

    [SerializeField]
    private GameEvent questJournalEvent;

    [SerializeField]
    private GameEvent interactionEvent;

    [SerializeField]
    private GameEvent enterCombatEvent;

    [SerializeField]
    private GameEvent exitCombatEvent;

    [SerializeField]
    private GameEvent playerAttackEvent;

    [SerializeField]
    private GameEvent changeCursorToCombatModeEvent;

    [SerializeField]
    private GameEvent changeCursorToNormalModeEvent;

    [SerializeField]
    private AttributesController attributesController;

    [SerializeField]
    private CombatController combatController;

    [SerializeField]
    private InventoryController inventoryController;

    [SerializeField]
    private QuestJournalController questJournalController;

    [SerializeField]
    private Animator characterAnimator;

    [SerializeField]
    private Transform interactionTrigger;

    [SerializeField]
    private Vector3 interactionTriggerDownOffset;

    [SerializeField]
    private Vector3 interactionTriggerUpOffset;

    [SerializeField]
    private Vector3 interactionTriggerLeftOffset;

    [SerializeField]
    private Vector3 interactionTriggerRightOffset;

    [SerializeField]
    private GameObject gameOverScreen;

    private float moveLimiter = 0.7f;


    public float runSpeed = 5.0f;

    Vector2 movement;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        applicationStates.uiIsOpen = false;
    }

    void Update()
    {
        if(attributesController.CurrentHealth > 0)
        {
            movement = Vector2.zero;

            if (!applicationStates.uiIsOpen && !combatController.IsMovmentLocked())
            {
                // Gives a value between -1 and 1
                movement.x = Input.GetAxisRaw("Horizontal"); // -1 is left
                movement.y = Input.GetAxisRaw("Vertical"); // -1 is down

                characterAnimator.SetFloat("Horizontal", movement.x);
                characterAnimator.SetFloat("Vertical", movement.y);
                characterAnimator.SetFloat("Speed", movement.sqrMagnitude);

                if (movement.y < 0)
                {
                    // Debug.Log("DOWN");
                    interactionTrigger.localPosition = interactionTriggerDownOffset;
                    characterAnimator.SetInteger("LastWalkDirection", 0);
                }

                if (movement.y > 0)
                {
                    //Debug.Log("UP");
                    interactionTrigger.localPosition = interactionTriggerUpOffset;
                    characterAnimator.SetInteger("LastWalkDirection", 1);
                }

                if (movement.x < 0)
                {
                    // Debug.Log("LEFT");
                    interactionTrigger.localPosition = interactionTriggerLeftOffset;
                    characterAnimator.SetInteger("LastWalkDirection", 2);
                }

                if (movement.x > 0)
                {
                    //Debug.Log("RIGHT");
                    interactionTrigger.localPosition = interactionTriggerRightOffset;
                    characterAnimator.SetInteger("LastWalkDirection", 3);
                }

                if(movement.magnitude > 0.0f)
                {
                    gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                }
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                characterAnimator.SetFloat("Horizontal", movement.x);
                characterAnimator.SetFloat("Vertical", movement.y);
                characterAnimator.SetFloat("Speed", movement.sqrMagnitude);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!combatController.IsInCombat)
                {
                    interactionEvent.Raise();
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!questJournalController.IsQuestJournalOpen)
                {
                    if(inventoryController.InventoryIsOpen)
                    {
                        if(combatController.IsInCombat)
                        {
                            changeCursorToCombatModeEvent.Raise();
                        }
                        else
                        {
                            changeCursorToNormalModeEvent.Raise();
                        }
                    }
                    else
                    {
                        changeCursorToNormalModeEvent.Raise();
                    }
                    showInventory.Raise();
                }
                
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                playerUseActiveItem.Raise();
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                lastItemBarSlot.Raise();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                nextItemBarSlot.Raise();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (!inventoryController.InventoryIsOpen)
                {
                    if (questJournalController.IsQuestJournalOpen)
                    {
                        if (combatController.IsInCombat)
                        {
                            changeCursorToCombatModeEvent.Raise();
                        }
                        else
                        {
                            changeCursorToNormalModeEvent.Raise();
                        }
                    }
                    else
                    {
                        changeCursorToNormalModeEvent.Raise();
                    }
                    questJournalEvent.Raise();
                }
               
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!inventoryController.InventoryIsOpen && !questJournalController.IsQuestJournalOpen)
                {
                    if (applicationStates.isInCombatMode)
                    {
                        exitCombatEvent.Raise();
                    }
                    else
                    {
                        enterCombatEvent.Raise();
                    }
                } 
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if(combatController.IsInCombat && !inventoryController.InventoryIsOpen && !questJournalController.IsQuestJournalOpen)
                {
                    playerAttackEvent.Raise();
                }
            }

        }
        else
        {
            playerDeadEvent.Raise();
            gameOverScreen.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (movement.x != 0 && movement.y != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            movement *= moveLimiter;
        }
        
        body.velocity = movement * runSpeed * Time.deltaTime;
    }
}
