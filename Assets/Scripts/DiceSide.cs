using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySprite;
    private DiceRoll parentDice;
    [HideInInspector] public int sideId;
    private bool isDown = false;
    private float coolDownCheck = 0;

    private void Awake()
    {
        parentDice = gameObject.GetComponentInParent<DiceRoll>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "MapWall")
        {
            if (parentDice.diceVelocity.x <= 0.01f && parentDice.diceVelocity.y <= 0.01f && parentDice.diceVelocity.z <= 0.01f && !isDown)
            {
                coolDownCheck += Time.deltaTime;
                if(coolDownCheck >= 1.5f)
                {
                    isDown = true;
                    parentDice.FaceDownSide(sideId);
                }
            }else if (!isDown && coolDownCheck != 0)
            {
                coolDownCheck = 0;
            }
        }
    }

    public void SetSprite(Sprite theSprite, int myId)
    {
        mySprite.sprite = theSprite;
        sideId = myId;
    }
}
