using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update

    public string text {
        get {
            return textMeshPro.text;
        }
        set {
            textMeshPro.text = value;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
