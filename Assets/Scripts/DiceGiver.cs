using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGiver : MonoBehaviour
{
    enum DiceType {Status, Fight, Shoot, Interaction};
    [SerializeField] private DiceType Dice;
    [SerializeField] private Sprite emptySprite;
    private bool isDiceGiven = false;

    public void GiveDice(){
        if(!isDiceGiven) {
            switch(Dice) {
                case DiceType.Status : DiceRack.Instance.addStatusDice();  break;
                case DiceType.Fight : DiceRack.Instance.addFightDice();  break;
                case DiceType.Interaction : DiceRack.Instance.addInteractionDice();  break;
                case DiceType.Shoot : DiceRack.Instance.addShootDice();  break;
            }
            isDiceGiven = true;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if(spriteRenderer != null && emptySprite != null) {
                spriteRenderer.sprite = emptySprite;
            }

            GameManager.Instance.OpenDoor();
        }
    }
        
}
