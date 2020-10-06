using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatController : MonoBehaviour
{
    [SerializeField]
    private GameEvent changeCursorToCombatModeEvent;

    [SerializeField]
    private GameEvent changeCursorToNormalModeEvent;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private CombatSystem combatSystem;

    [SerializeField]
    private AttributesController attributesController;

    [SerializeField]
    private GameObject aimingHelperObject;

    [SerializeField]
    private GameObject endturnButton;

    [SerializeField]
    private GameObject hudMovementBar;

    [SerializeField]
    private GameObject hudTurnIndicator;

    [SerializeField]
    private GameObject hudActionIndicator;

    [SerializeField]
    private string shooterName;

    [SerializeField]
    private InventoryController inventoryController;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private PlayerMessageSystem messageSystem;

    [SerializeField]
    private Weapon unarmedWeapon;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private WeaponProjectile weaponProjectile;

    [SerializeField]
    private float bulletForce = 10f;

    [SerializeField]
    private bool readyToAttack;

    [SerializeField]
    private bool isOnTurn;

    [SerializeField]
    private bool isInCombat;

    [SerializeField]
    private bool isTurnFinished;

    [SerializeField]
    private bool hasDoneAction;

    [SerializeField]
    private bool isMovmentLocked;

    [SerializeField]
    private Vector3 tempPosition;

    [SerializeField]
    private float delta;

    [SerializeField]
    private float movementSpeed;

    public bool IsOnTurn { get => isOnTurn; set => isOnTurn = value; }
    public bool HasDoneAction { get => hasDoneAction; set => hasDoneAction = value; }

    public float Delta { get => delta; set => delta = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool IsInCombat { get => isInCombat; set => isInCombat = value; }
    public Weapon Weapon { get => weapon; set => weapon = value; }
    public WeaponProjectile WeaponProjectile { get => weaponProjectile; set => weaponProjectile = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnTurn && !isMovmentLocked)
        {

            Delta += (playerTransform.position - tempPosition).magnitude;

            tempPosition = playerTransform.position;

            if(Delta >= MovementSpeed)
            {
                isMovmentLocked = true;
            }
        }



    }

    public void EnterCombatMode()
    {
        System.Random rnd = new System.Random();
        float turnOrderIndex = rnd.Next(1, 21) + attributesController.DerivedAttributes.AgilityModifier;
        MovementSpeed = 10 + attributesController.DerivedAttributes.AgilityModifier;

        combatSystem.AddCombatant(shooterName, turnOrderIndex, EnterTurn, ExitTurn, ExitRound, ExitCombat, IsTurnFinished);
        IsInCombat = true;

        hudMovementBar.SetActive(true);
        hudTurnIndicator.SetActive(true);
        hudActionIndicator.SetActive(true);
    }

    public void ExitCombatMode()
    {
        if(combatSystem.CanExitCombat())
        {
            combatSystem.ExitCombatMode();
        }
       
    }

    public void EnterTurn()
    {
        aimingHelperObject.SetActive(true);
        endturnButton.SetActive(true);
        
        IsOnTurn = true;
        changeCursorToCombatModeEvent.Raise();
        tempPosition = playerTransform.position;
        isMovmentLocked = false;
        Delta = 0;
    }

    public void ExitTurn()
    {
        aimingHelperObject.SetActive(false);
        endturnButton.SetActive(false);
        /*hudMovementBar.SetActive(false);
        hudTurnIndicator.SetActive(false);
        hudActionIndicator.SetActive(false);*/
        IsOnTurn = false;
        isMovmentLocked = true;
        changeCursorToNormalModeEvent.Raise();
        ExitCombatMode();
    }

    public void ExitRound()
    {
        isTurnFinished = false;
        HasDoneAction = false;
    }

    public void ExitCombat()
    {
        aimingHelperObject.SetActive(false);
        endturnButton.SetActive(false);
        hudMovementBar.SetActive(false);
        hudTurnIndicator.SetActive(false);
        hudActionIndicator.SetActive(false);
        IsOnTurn = false;
        IsInCombat = false;
        isMovmentLocked = false;
        isTurnFinished = false;
        HasDoneAction = false;
        changeCursorToNormalModeEvent.Raise();
    }

    public void Attack()
    {
        if (IsOnTurn && !HasDoneAction)
        {
            if (Weapon != null)
            {
                if (Weapon.weaponIsRanged)
                {
                    if (WeaponProjectile != null)
                    {
                        if (WeaponProjectile.weapon == Weapon)
                        {
                            if (inventoryController.GetItemCount(WeaponProjectile) > 0)
                            {
                                readyToAttack = true;
                            }
                            else
                            {
                                readyToAttack = false;
                                messageSystem.ShowPlayerMessage("No projectiles!");
                            }
                        }
                        else
                        {
                            readyToAttack = false;
                            messageSystem.ShowPlayerMessage("The projectile does not match!");
                        }
                    }
                    else
                    {
                        readyToAttack = false;
                        messageSystem.ShowPlayerMessage("No projectiles!");
                    }
                }
                else
                {
                    readyToAttack = true;
                }
            }
            else
            {
                Weapon = unarmedWeapon;
                readyToAttack = true;
            }

            if (readyToAttack)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0.0f;

                Vector3 playerPos = transform.position;
                playerPos.z = 0.0f;

                if ((mousePos - playerPos).magnitude <= Weapon.weaponRange)
                {
                    RaycastHit2D hit;
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, LayerMask.GetMask("Hitbox"));
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "HitboxTrigger")
                        {
                            
                            if (Weapon.weaponIsRanged)
                            {
                                hit.collider.GetComponent<HitboxTrigger>().HitTarget(shooterName, Weapon, WeaponProjectile);
                                GameObject projectile = Instantiate(WeaponProjectile.projectilePrefab, firePoint.position, firePoint.rotation);
                                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                                inventoryController.RemoveInventoryItem(WeaponProjectile, 1);
                            }
                            else
                            {
                                hit.collider.GetComponent<HitboxTrigger>().HitTarget(shooterName, Weapon, null);
                            }
                            HasDoneAction = true;
                            aimingHelperObject.SetActive(false);
                            changeCursorToNormalModeEvent.Raise();
                        }
                        else
                        {
                            messageSystem.ShowPlayerMessage("Not a valid target!");
                        }
                    }
                }
                else
                {
                    messageSystem.ShowPlayerMessage("Target is out of range!");
                }
            }
        }
    }

    public bool IsTurnFinished()
    {
        return isTurnFinished;
    }

    public bool IsMovmentLocked()
    {
        return isMovmentLocked;
    }

    public void SetIsTurnFinished(bool isTurnFinished)
    {
        this.isTurnFinished = isTurnFinished;
    }

    public void OnPlayerDeath()
    {
        combatSystem.RemoveCombatant(shooterName);
        ExitCombatMode();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.Weapon = weapon;
    }

    public void SetWeaponProjectile(WeaponProjectile weaponProjectile)
    {
        this.WeaponProjectile = weaponProjectile;
    }
}
