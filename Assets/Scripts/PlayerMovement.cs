using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform weaponRotationPoint;
    [SerializeField]
    private Transform spriteRenderer;

    private Animator animator;

    private Vector2 playerMovement;
    private Vector2 mousePosition;
    private bool facingRight = true;

    void Awake(){
       animator = spriteRenderer.GetComponent<Animator>();
    }

    void Start()
    {
        if(myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
    }


    void Update()
    {
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");

        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if(playerMovement.x  != 0 || playerMovement.y != 0){
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {
        // Movement
        myRigidbody.MovePosition(myRigidbody.position + playerMovement * PlayerStats.Instance.runSpeed * Time.deltaTime);

        Vector2 lookDirection = mousePosition - new Vector2(weaponRotationPoint.position.x, weaponRotationPoint.position.y);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        weaponRotationPoint.rotation = Quaternion.Euler(0, 0, angle);

        //flip
        if(angle > -90 && angle<= 90 && facingRight == false)
        {
            FlipSprite(weaponRotationPoint, new Vector3(1,-1,1));
            FlipSprite(spriteRenderer, new Vector3(-1, 1, 1));
            facingRight = !facingRight;
        }
        else if(angle <= -90 && facingRight == true || angle > 90 && facingRight == true)
        {
            FlipSprite(weaponRotationPoint, new Vector3(1, -1, 1));
            FlipSprite(spriteRenderer, new Vector3(-1, 1, 1));
            facingRight = !facingRight;
        }
    }


    private void FlipSprite(Transform whoToFlip, Vector3 howToFlip)
    {
        Vector3 scaleToFlip = whoToFlip.localScale;
        scaleToFlip.x *= howToFlip.x;
        scaleToFlip.y *= howToFlip.y;
        scaleToFlip.z *= howToFlip.z;
        whoToFlip.localScale = scaleToFlip;
    }
}
