using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DiceRack : MonoBehaviour
{
    private List<Dice> diceRack;

    // Start is called before the first frame update
    void Awake()
    {
          Dice one = new Dice();
          one.CreateInteractionDice();
          AddDice(one);

          Dice two = new Dice();
          two.CreateShootDice();
          AddDice(two);

          Dice three = new Dice();
          three.CreateStatusDice();
          AddDice(three);

          Reset();
          Randomize();
    }

    public void AddDice(Dice newDice){
        diceRack.Add(newDice);
    }

    public void Reset(){
        PlayerStats.Instance.Reset();
    }

    public void Randomize(){
        for(int index = 0; index < diceRack.Count; index++ ) {
            diceRack[index].SelectRandomFace();
        }
    }
}

