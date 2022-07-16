using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = PlayerStats.Instance.currentHP /  PlayerStats.Instance.maxHP;
        rectTransform.sizeDelta = new Vector2 (ratio*50, 16);
    }
}
