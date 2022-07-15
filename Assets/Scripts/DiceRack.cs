using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DiceRack : MonoBehaviour
{
    private List<Dice> diceRack;

    // Start is called before the first frame update
    void Awake()
    {
          
    }

    public AddDice(Dice newDice){
        diceRack.Add(newDice)
    }

    public Reset(){
        Bullet.reset();
    }

    public Ramdomize(){
        for(int index = 0; i++; diceRack < diceRack.Count) {
            diceRack[index].SelectRandomFace();
        }
    }
}

