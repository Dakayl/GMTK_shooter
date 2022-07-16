using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void Update()
    {
        //TO DO replace with proper event mgt
        float ratio = PlayerStats.Instance.currentHP /  PlayerStats.Instance.maxHP;
        rectTransform.sizeDelta = new Vector2 (ratio*100, 16);
    }
}
