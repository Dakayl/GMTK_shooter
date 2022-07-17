using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DiceUI : MonoBehaviour
{
    [SerializeField] private Image image;
    // Start is called before the first frame update

   public Sprite sprite {
        get {
            return image.sprite;
        }
        set {
            image.sprite = value;
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
