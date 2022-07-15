using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DiceRack : MonoBehaviour
{
    private List<Dice> diceRack;

    private List<DiceFace> currentFaces;

    // Start is called before the first frame update
    void Awake()
    {
          
          diceRack = new List<Dice>();
          Reset();
          
          Dice one = new Dice();
          one.CreateInteractionDice();
          AddDice(one);

          Dice two = new Dice();
          two.CreateShootDice();
          AddDice(two);

          Dice three = new Dice();
          three.CreateStatusDice();
          AddDice(three);
         
          Randomize();
    }

    public void AddDice(Dice newDice){
        diceRack.Add(newDice);
    }

    public void Reset(){
        PlayerStats.Instance.ResetStats();
    }

    public void Randomize(){
        currentFaces = new List<DiceFace>();
        for(int index = 0; index < diceRack.Count; index++ ) {
            DiceFace face = diceRack[index].SelectRandomFace();
            currentFaces.Add(face);
        }
        DebugMe();
    }

    public void DebugMe(){
        Debug.Log(this);
        Debug.Log(PlayerStats.Instance.isPiercingActivated+"-"+PlayerStats.Instance.isBouncingActivated);
        Debug.Log(PlayerStats.Instance.isFireActivated+"-"+PlayerStats.Instance.isPoisonActivated);
        Debug.Log(PlayerStats.Instance.attackDamage+"/"+PlayerStats.Instance.attackSpeed+"/"+PlayerStats.Instance.attackRange);
    }

    public override string ToString()
    {
        string str = "";
        for(int index = 0; index < diceRack.Count; index++ ) {
           str += diceRack[index] + " - ";
        }
        return str;
    }
}

