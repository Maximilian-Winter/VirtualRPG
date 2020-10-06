using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCombatController : MonoBehaviour
{

    [SerializeField]
    private Transform npcTransform;

    [SerializeField]
    private NPCController npcController;

    [SerializeField]
    private CombatSystem combatSystem;

    [SerializeField]
    private AttributesController attributesController;

    [SerializeField]
    private HitboxTrigger hitboxTrigger;

    [SerializeField]
    private HitboxTrigger hitboxTriggerTarget;

    [SerializeField]
    private string shooterName;

    [SerializeField]
    private Transform firePoint;

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
    private bool isTurnFinished;

    [SerializeField]
    private bool isInCombat;

    [SerializeField]
    private bool isDead;

    [SerializeField]
    private bool hasAttacked;

    [SerializeField]
    private bool isMovmentLocked;

    [SerializeField]
    private bool isMoving;

    [SerializeField]
    private Vector3 tempPosition;

    [SerializeField]
    private float delta;

    [SerializeField]
    private float movementSpeed;

    public bool IsMovmentLocked { get => isMovmentLocked; set => isMovmentLocked = value; }
    public Vector3 TempPosition { get => tempPosition; set => tempPosition = value; }
    public float Delta { get => delta; set => delta = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }


    // Start is called before the first frame update
    void Start()
    {
        isMovmentLocked = false;
    }

    private void OnEnable()
    {
        hitboxTrigger.OnHit += EnterCombatMode;
    }

    private void OnDisable()
    {
        hitboxTrigger.OnHit -= EnterCombatMode;
    }
    // Update is called once per frame
    void Update()
    {
        if(attributesController.CurrentHealth > 0)
        {
            if (isOnTurn && !hasAttacked)
            {
                if (weapon != null)
                {
                    if (weapon.weaponIsRanged)
                    {
                        if (weaponProjectile != null)
                        {
                            readyToAttack = true;
                        }
                        else
                        {
                            readyToAttack = false;
                        }
                    }
                    else
                    {
                        readyToAttack = true;
                    }
                }
                else
                {
                    readyToAttack = false;
                }

                if (readyToAttack)
                {
                    Vector3 targetPos = hitboxTriggerTarget.gameObject.transform.position;
                    targetPos.z = 0.0f;

                    Vector3 playerPos = transform.position;
                    playerPos.z = 0.0f;

                    if ((targetPos - playerPos).magnitude <= weapon.weaponRange)
                    {
                        NPCAttack();
                        if (isMoving)
                        {
                            npcController.SetFollowTargetOff();
                            isMoving = false;
                        }
                    }
                    else
                    {
                        if (!isMoving)
                        {
                            npcController.SetFollowTargetOn();
                            isMoving = true;
                        }
                        if (isMovmentLocked)
                        {
                            isTurnFinished = true;
                        }
                    }

                    Delta += (npcTransform.position - tempPosition).magnitude;

                    tempPosition = npcTransform.position;

                    if (Delta >= MovementSpeed)
                    {
                        isMovmentLocked = true;
                    }
                }
            }
        }
        else
        {
            if(!isDead)
            {
                OnDeath();
                isDead = true;
            }
        }
        
    }

    void NPCAttack()
    {
        Attack();
    }

    public void Attack()
    {
        if (isOnTurn)
        {
            if (weapon.weaponIsRanged)
            {
                hitboxTriggerTarget.HitTarget(shooterName, weapon, weaponProjectile);
                GameObject projectile = Instantiate(weaponProjectile.projectilePrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            }
            else
            {
                hitboxTriggerTarget.HitTarget(shooterName, weapon, null);
            }
            isTurnFinished = true;
            hasAttacked = true;

        }
    }

    public void EnterTurn()
    {
        isOnTurn = true;
        hasAttacked = false;
        Delta = 0.0f;
        isMovmentLocked = false;
    }

    public void ExitTurn()
    {
        isOnTurn = false;
    }

    public void ExitRound()
    {
        isTurnFinished = false;
    }

    public bool IsTurnFinished()
    {
        return isTurnFinished;
    }

    public void EnterCombatMode()
    {
        if(!isInCombat)
        {
            System.Random rnd = new System.Random();

            float turnOrderIndex = rnd.Next(1, 21) + attributesController.DerivedAttributes.AgilityModifier;
            movementSpeed = 10 + attributesController.DerivedAttributes.AgilityModifier;

            combatSystem.AddCombatant(shooterName, turnOrderIndex, EnterTurn, ExitTurn, ExitRound, ExitCombatMode, IsTurnFinished);
            isInCombat = true;
            tempPosition = npcTransform.position;
            Delta = 0.0f;
        }
       
    }

    public void ExitCombatMode()
    {
        isOnTurn = false;
        hasAttacked = false;
        Delta = 0.0f;
        isMovmentLocked = false;
        if (isMoving)
        {
            npcController.SetFollowTargetOff();
            isMoving = false;
        }
    }

    void OnDeath()
    {
        combatSystem.RemoveCombatant(shooterName);
        ExitCombatMode();

        StartCoroutine(DisableNPC());
    }

    IEnumerator DisableNPC()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
