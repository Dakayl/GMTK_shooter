using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class DiceRack : MonoBehaviour
{
    [SerializeField] private GameObject diceUIPrefab;
    [SerializeField] private EffectWindowUI effectWindowUI;
    private List<Dice> diceRack;
    private List<DiceFace> currentFaces;
    private List<GameObject> diceUIList;
    public void Awake()
    {
         
          diceRack = new List<Dice>();
          Dice one = new Dice();
          one.CreateInteractionDice();
          AddDice(one);
          
          Dice two = new Dice();
          two.CreateShootDice();
          AddDice(two);

          Dice three = new Dice();
          three.CreateStatusDice();
          AddDice(three);

          Dice four = new Dice();
          four.CreateFightDice();
          AddDice(four);

          CreateDicesUI();

    }
    public void Start()
    {
        
          Reset();   
          Randomize();
          ShowDicesUI();
          effectWindowUI.changeDices(diceRack);
    }

     // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            effectWindowUI.Show();
        }

        if(Input.GetKeyUp(KeyCode.Tab)) {
             effectWindowUI.Hide();
        }
    }

    private void cleanDiceUIList(){
        if(diceUIList != null) {
            for(int index = 0; index < diceUIList.Count; index++) {
                Destroy(diceUIList[index]);
            }
        }
        diceUIList = new List<GameObject>();
    }

    public void CreateDicesUI(){
        cleanDiceUIList();
        int diceNumbers = diceRack.Count;
        int posx = (- diceNumbers/2*50);
        if(diceNumbers%2 == 0) {
            posx += 25;
        }
        for(int index = 0; index < diceNumbers; index++ ) {
            GameObject diceUI = Instantiate(diceUIPrefab);
            diceUI.transform.SetParent(this.gameObject.transform, false);
            RectTransform transform = diceUI.GetComponent<RectTransform>();
          
            if(transform) {
                transform.anchoredPosition = new Vector3(posx, y: 25, 0);
                posx += 50;
            }
            diceUIList.Add(diceUI);
        }
    }

    public void ShowDicesUI(){
        for(int index = 0; index < diceUIList.Count; index++ ) {
            DiceUI ui = diceUIList[index].GetComponent<DiceUI>();
            if(ui) {
                ui.text = diceRack[index].ToString(); //TO DO verify diceRack[index] exists
            } 
        }
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

