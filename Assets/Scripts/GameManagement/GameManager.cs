using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton

    [SerializeField]
    private GameObject[] dontDestroyOnLoad;
    [SerializeField]
    private int firstLevelToLoad = 1;
    [SerializeField]
    private Animator UIAnim;

    [HideInInspector]
    public int currentLevel = 0;
    [HideInInspector]
    public int enemyKilled = 0;

    private int lastLevelIndex;
    private int enemyInLevel = 0;
    private int enemyKilledInLevel = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager Error");
            Destroy(gameObject);
            return;
        }
        Instance = this;


        for (int i = 0; i < dontDestroyOnLoad.Length; i++)
        {
            DontDestroyOnLoad(dontDestroyOnLoad[i]);
        }

        LoadNewLevel(firstLevelToLoad);
    }
    private void Start()
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

    public void LoadNewLevel()
    {
        currentLevel++;
        int nextLevelIndex = Random.Range(1, SceneManager.sceneCount);
        if(SceneManager.sceneCount -1 > 1)
        {
            while (nextLevelIndex == lastLevelIndex)
            {
                nextLevelIndex = Random.Range(1,SceneManager.sceneCount);
            }
        }
        SceneManager.LoadScene(nextLevelIndex);
        enemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyKilledInLevel = 0;
    }
    public void LoadNewLevel(int levelIndex)
    {
        currentLevel++;
        SceneManager.LoadScene(levelIndex);
        enemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyKilledInLevel = 0;
    }

    public void GoToNextFloor()
    {
        if(enemyKilledInLevel >= enemyInLevel)
        {
            UIAnim.Play("ScreenTransition");
        }
    }

    public void NewEnemyKilled()
    {
        enemyKilled++;
        enemyKilledInLevel++;
    }
}
