using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class DiceRack : MonoBehaviour
{
    [SerializeField] private GameObject diceUIPrefab;
    [SerializeField] private EffectWindowUI effectWindowUI;
    [SerializeField] private GameObject dice3DPrefab;
    [SerializeField] private Transform dice3DParent;
    public static DiceRack Instance { get; private set; } // Singleton
    private List<Dice> diceRack;
    private List<DiceFace> currentFaces;
    private List<GameObject> diceUIList;
    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple DiceRack Error");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        diceRack = new List<Dice>();
        CreateDicesUI();
    }

    public void addInteractionDice(){
        Dice one = new Dice();
        one.CreateInteractionDice();
        AddDice(one);
        CreateDicesUI();
        RandomizeRealtime();
    }
    public void addShootDice(){
         Dice two = new Dice();
        two.CreateShootDice();
        AddDice(two);
        CreateDicesUI();
        RandomizeRealtime();

    }
    public void addStatusDice(){
       Dice three = new Dice();
        three.CreateStatusDice();
        AddDice(three);
        CreateDicesUI();
        RandomizeRealtime();
    }

    public void addFightDice(){
        Dice four = new Dice();
        four.CreateFightDice();
        AddDice(four);
        CreateDicesUI();
        RandomizeRealtime();
    }

    public void Start()
    {
        
        //Reset();
        //RandomizeRealtime();
        //Randomize();
        //ShowDicesUI();
        //effectWindowUI.changeDices(diceRack);
    }

     // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            effectWindowUI.Show();
            GameManager.Instance.Pause();
        }

        if(Input.GetKeyUp(KeyCode.Tab)) {
             effectWindowUI.Hide();
             GameManager.Instance.Resume();
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
            diceUI.SetActive(false);
        }
    }

    public void ShowDicesUI(){
        if(diceUIList != null)
        {
            for(int index = 0; index < diceUIList.Count; index++ ) {
                GameObject gameObjectDiceUI = diceUIList[index];
                DiceUI ui = gameObjectDiceUI.GetComponent<DiceUI>();
                if(ui && diceRack[index] != null) {
                    ui.sprite = diceRack[index].sprite; //TO DO verify diceRack[index] exists
                }
                gameObjectDiceUI.SetActive(true);
                
            }
        }
    }

    public void AddDice(Dice newDice){
        diceRack.Add(newDice);
        
    }    

    public void RandomizeRealtime()
    {
        currentFaces = new List<DiceFace>();
        for (int index = 0; index < diceRack.Count; index++)
        {
            GameObject dice3D = Instantiate(dice3DPrefab);
            dice3D.transform.parent = dice3DParent;
            dice3D.GetComponent<DiceRoll>().SetFaces( diceRack[index]);
        }
    }

    public void NewCurrentFace(DiceFace face)
    {
        currentFaces.Add(face);
        if(currentFaces.Count == diceRack.Count) {
            ShowDicesUI();
            effectWindowUI.changeDices(diceRack);
        }
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

