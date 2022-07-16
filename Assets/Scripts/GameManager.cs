using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance { get; private set; } // Singleton
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager Error");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Pause ()
    {
        Time.timeScale = 0;
    }
    public void Resume ()
    {
        Time.timeScale = 1;
    }
}
