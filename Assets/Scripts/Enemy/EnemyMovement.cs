using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private enum movementType
    {
        rushing,
        shooting
    }
    [Header("General")]
    [SerializeField]
    private movementType myMovementType = movementType.rushing;
    [SerializeField]
    private float runSpeed = 4;
    [SerializeField]
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private Transform mySprite;
    [SerializeField]
    private Transform weaponRotationPoint;
    [SerializeField]
    private Enemy myStats;

    private Transform playerTransform;
    private bool hasSeenPlayer = false;
    private float playerDetectionRange;
    private float playerAttackRange;
    private Vector2 myMovement;
    private bool canMove = true;
    [HideInInspector] public bool isStunned = false;
    private Coroutine canMoveCooldown;
    private bool isFacingRight = true;
    [SerializeField] private Animator animator;


    // Patrol related
    private float patrolYoyoTimer;
    private float patrolYoyoNextStep = 1;

    //Rushing related
    [Header("Rushing Related")]
    [SerializeField]
    private Animator meleeWeaponAnimator;
    [SerializeField]
    private Transform[] hitBoxesLocation;
    [SerializeField]
    private float[] hitBoxesRange;

    //Shooting related
    [Header("Shooting")]
    [SerializeField]
    private GameObject projectilePrefab;
    private float shootCooldown = 1;
    private bool moveLikeJagger = false;
    private bool fleeing = false;
    private bool canShoot = true;
   

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        patrolYoyoTimer -= Random.Range(-2, 0);     // Offset random generation
        if(myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
        if (myStats == null)
        {
            myStats = GetComponent<Enemy>();
        }
        switch (myMovementType)
        {
            case movementType.rushing:
                playerDetectionRange = 5.5f;
                playerAttackRange = 1.5f;
                break;
            case movementType.shooting:
            default:
                playerDetectionRange = 5.5f;
                playerAttackRange = 4f;
                break;

        }

        if (Random.Range(0, 2) == 0)
            moveLikeJagger = false;
        else
            moveLikeJagger = true;

    }
    void Update()
    {
        if (!hasSeenPlayer)
        {
            Patroling();
            if (Physics2D.OverlapCircle(transform.position, playerDetectionRange, LayerMask.GetMask("Player")))
            {
                hasSeenPlayer = true;
                StopAllCoroutines();
            }
        }
        else
        {
            switch (myMovementType)
            {
                case movementType.rushing:
                    Rushing();
                    return;
                case movementType.shooting:
                    Shooting();
                    return;
                default:
                    return;
            }
        }
    }

    private void FixedUpdate()
    {
        // Movement
        myRigidbody.MovePosition(myRigidbody.position + myMovement * runSpeed * Time.deltaTime);

        //flip
        if ((myMovement.x > 0 && !isFacingRight) || (myMovement.x < 0 && isFacingRight))
        {
            Vector3 scaleToFlip = mySprite.localScale;
            scaleToFlip.x *= -1;
            mySprite.localScale = scaleToFlip;
            isFacingRight = !isFacingRight;
        }
    }

    public void TookDamage()
    {
        myMovement = Vector2.zero;
        if (!hasSeenPlayer)
        {
            hasSeenPlayer = true;
            StopAllCoroutines();
        }
        CanMoveBlocking(0.07f);
    }

    private void Patroling()
    {
        if (canMove && !isStunned)
        {
            if(myMovement == Vector2.zero)  // Only count when not moving
            {
                patrolYoyoTimer += Time.deltaTime;
                animator.SetBool("isMoving", false);
            }
            if(patrolYoyoTimer >= patrolYoyoNextStep)
            {
                patrolYoyoTimer = 0;
                patrolYoyoNextStep = Random.Range(0.5f, 4);

                if(Random.Range(0,3) == 0)
                {
                    myMovement = GetPlayerDirection();
                }
                else
                {
                    myMovement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                }

                myMovement *= 0.4f;
                animator.SetBool("isMoving", true);
                StartCoroutine(moveForHowLong(Random.Range(0.5f, 2)));
            }
        }
    }

    private void Rushing()
    {
        if (canMove && !isStunned)
        {
            myMovement = GetPlayerDirection();
            animator.SetBool("isMoving", true);
            if(Physics2D.OverlapCircle(transform.position, playerAttackRange, LayerMask.GetMask("Player")))
            {
                StartCoroutine(MeleeAttack());
            }

        }
    }

    private void Shooting()
    {
        if (!isStunned && canMove)
        {
            if (Physics2D.OverlapCircle(transform.position, playerAttackRange, LayerMask.GetMask("Player")))
            {
                if(!canShoot)
                {
                    if (Physics2D.OverlapCircle(transform.position, playerAttackRange*0.75f, LayerMask.GetMask("Player")))
                    {
                        fleeing = true;
                        myMovement = GetPlayerDirection() * -1;
                    }
                    else
                    {
                        if (fleeing)
                        {
                            myMovement = GetPlayerDirection() * -1;
                            myMovement = myMovement + PerpendicularVector(myMovement, moveLikeJagger);
                            animator.SetBool("isMoving", true);
                        }
                        else
                        {
                            myMovement = GetPlayerDirection();
                            myMovement = myMovement + PerpendicularVector(myMovement, moveLikeJagger);
                            animator.SetBool("isMoving", true);
                        }
                    }
                }
                else
                {
                    if(canShoot) {
                        StartCoroutine(RangeAttack());
                    }
                        
                }
            }
            else
            {
                fleeing = false;
                myMovement = GetPlayerDirection();
                animator.SetBool("isMoving", true);
            }
            myMovement.Normalize();
        }
    }

    private Vector2 PerpendicularVector(Vector2 theVector, bool goClockwise)
    {
        if (goClockwise)
        {
            return new Vector2(theVector.y, -theVector.x);
        }
        else
        {
            return new Vector2(-theVector.y, theVector.x);
        }
    }

    private Vector2 GetPlayerDirection()
    {
        return new Vector2(
                    playerTransform.position.x - gameObject.transform.position.x,
                    playerTransform.position.y - gameObject.transform.position.y)
                    .normalized;
    }


    private void CanMoveBlocking(float duration)
    {
        if (canMoveCooldown != null)
            StopCoroutine(canMoveCooldown);
        canMoveCooldown = StartCoroutine(BlockCanMove(duration));
    }

    private IEnumerator moveForHowLong(float duration)
    {
        yield return new WaitForSeconds(duration);
        myMovement = Vector2.zero;
    }
    private IEnumerator MeleeAttack()
    {
        animator.SetBool("isMoving", false);
        myMovement = Vector2.zero;
        CanMoveBlocking(1f);

        Vector2 lookDirection =
            new Vector2(playerTransform.position.x, playerTransform.position.y)
            - new Vector2(weaponRotationPoint.position.x, weaponRotationPoint.position.y);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        weaponRotationPoint.rotation = Quaternion.Euler(0, 0, angle);

        meleeWeaponAnimator.Play("SwordAttack Buildup");

        yield return new WaitForSeconds(0.3f);

        meleeWeaponAnimator.Play("SwordAttack Swing");

        Collider2D playerHit = null;

        for (int i = 0; i < hitBoxesLocation.Length; i++)
        {
            if(playerHit == null)
                playerHit = Physics2D.OverlapCircle(hitBoxesLocation[i].position, hitBoxesRange[i], LayerMask.GetMask("Player"));
        }

        if(playerHit != null)
        {
            myStats.DamagePlayer();
        }

    }

    private IEnumerator RangeAttack()
    {
        animator.SetBool("isMoving", false);
        canShoot = false;
        myMovement = Vector2.zero;
        CanMoveBlocking(1f);

        moveLikeJagger = !moveLikeJagger;

        Vector2 lookDirection =
            new Vector2(playerTransform.position.x, playerTransform.position.y)
            - new Vector2(weaponRotationPoint.position.x, weaponRotationPoint.position.y);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        weaponRotationPoint.rotation = Quaternion.Euler(0, 0, angle);

        meleeWeaponAnimator.Play("BowAttack Buildup");

        yield return new WaitForSeconds(0.3f);

        lookDirection =
            new Vector2(playerTransform.position.x, playerTransform.position.y)
            - new Vector2(weaponRotationPoint.position.x, weaponRotationPoint.position.y);
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        weaponRotationPoint.rotation = Quaternion.Euler(0, 0, angle);
        meleeWeaponAnimator.Play("BowAttack Shoot");


        GameObject enemyProjectile = Instantiate(projectilePrefab, weaponRotationPoint.position, weaponRotationPoint.rotation);
        enemyProjectile.GetComponent<Bullet>().damage = myStats.currentAttackDamage;
        Rigidbody2D bulletRB = enemyProjectile.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(weaponRotationPoint.right * 12, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);
        canShoot = true;

    }

    private IEnumerator BlockCanMove(float duration)
    {
        animator.SetBool("isMoving", false);
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

}
