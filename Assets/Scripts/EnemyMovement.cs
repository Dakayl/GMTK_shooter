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
    public bool isStunned = false;
    private Coroutine canMoveCooldown;
    private bool isFacingRight = true;

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
                playerDetectionRange = 6.5f;
                playerAttackRange = 1.5f;
                return;
            case movementType.shooting:
            default:
                playerDetectionRange = 6.5f;
                playerAttackRange = 4f;
                return;

        }
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
                StartCoroutine(moveForHowLong(Random.Range(0.5f, 2)));
            }
        }
    }

    private void Rushing()
    {
        if (canMove && !isStunned)
        {
            myMovement = GetPlayerDirection();

            if(Physics2D.OverlapCircle(transform.position, playerAttackRange, LayerMask.GetMask("Player")))
            {
                StartCoroutine(MeleeAttack());
            }

        }
    }

    private void Shooting()
    {

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
            Debug.Log("Damaging player : " );
            myStats.DamagePlayer();
        }

    }

    private IEnumerator BlockCanMove(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < hitBoxesLocation.Length; i++)
        {
            Gizmos.DrawSphere(hitBoxesLocation[i].position, hitBoxesRange[i]);
        }
    }
}
