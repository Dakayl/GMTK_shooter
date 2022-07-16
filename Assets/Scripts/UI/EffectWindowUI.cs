using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EffectWindowUI : MonoBehaviour
{
    [SerializeField] private GameObject dicePanelPrefab;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI killed;
    private List<GameObject> dicePanelList;

    private void cleanPanelList(){
         if(dicePanelList != null) {
            for(int index = 0; index < dicePanelList.Count; index++) {
                Destroy(dicePanelList[index]);
            }
        }
        dicePanelList = new List<GameObject>();
    }

    public void changeDices(List<Dice> diceRack) {
        cleanPanelList();
        int diceNumbers = diceRack.Count;
        int posy = (diceNumbers/2*60);
        if(diceNumbers%2 == 0) {
            posy -= 30;
        }
        for(int index = 0; index < diceRack.Count; index++ ) {
            GameObject dicePanel = Instantiate(dicePanelPrefab);
            DicePanelUI ui = dicePanel.GetComponent<DicePanelUI>();
            if(ui) {
                ui.icon = diceRack[index].ToString();
                ui.title = diceRack[index].getTitle();
                ui.effect = diceRack[index].getHtmlText();
            }
            
            dicePanel.transform.SetParent(this.gameObject.transform, false);
            RectTransform transform = dicePanel.GetComponent<RectTransform>();
          
            if(transform) {
                transform.anchoredPosition = new Vector3(5, y: posy, 0);
                posy -= 60;
            }
            dicePanelList.Add(dicePanel);
         
        }
    }

    public void Awake() {
    }

    public void Show(){
        killed.text = GameManager.Instance.ennemyKilled.ToString();
        level.text = GameManager.Instance.currentLevel.ToString();
        this.gameObject.SetActive(true);
    }

    public void Hide(){
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update

   
}