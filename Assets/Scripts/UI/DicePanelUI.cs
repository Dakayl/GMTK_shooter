using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DicePanelUI : MonoBehaviour
{
    [SerializeField] private DiceUI diceUI;
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI effectTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string icon {
        get {
            return diceUI.text;
        }
        set {
            diceUI.text = value;
        }
    }

    public string effect {
        get {
            return effectTxt.text;
        }
        set {
            effectTxt.text = value;
        }
    }

    public string title {
        get {
            return titleTxt.text;
        }
        set {
            titleTxt.text = value;
        }
    }
}
