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
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerShooting playerShooting ;

    [HideInInspector]
    public int currentLevel = 0;
    [HideInInspector]
    public int enemyKilled = 0;

    private int lastLevelIndex;
    private int enemyInLevel = 0;
    private int enemyKilledInLevel = 0;

    private bool isDead = false;

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

        
    }
    private void Start()
    {
        LoadNewLevel(firstLevelToLoad);
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Trying my best");
                UIAnim.Play("Reset");
            }
        }
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
        playerStats.gameObject.transform.position = GameObject.Find("PlayerStartPosition").transform.position;
        enemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyKilledInLevel = 0;
    }
    public void LoadNewLevel(int levelIndex)
    {
        currentLevel++;
        SceneManager.LoadScene(levelIndex);
        playerStats.gameObject.transform.position = GameObject.Find("PlayerStartPosition").transform.position;
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

    public void NewDiceLaunch()
    {
        //DiceRack.Instance.Reset();
        if(PlayerStats.Instance != null)
            PlayerStats.Instance.ResetStats();
        DiceRack.Instance.RandomizeRealtime();
    }

    public void PlayerDeath()
    {
        isDead = true;
        UIAnim.Play("GameOver");
        playerMovement.setMovement(false);
        playerShooting.setShooting(false);
    }

    public void NewEnemyKilled()
    {
        enemyKilled++;
        enemyKilledInLevel++;

        if (enemyKilledInLevel >= enemyInLevel)
        {
            OpenDoor();
        }
    }


    public void ResetGame()
    {
        isDead = false;
        playerMovement.setMovement(true);
        playerShooting.setShooting(true);
        playerStats.ResetStats();
        playerStats.HealToFull();
        currentLevel = 0;
        enemyKilled = 0;
        enemyInLevel = 0;
        enemyKilledInLevel = 0;


        UIAnim.Play("Default");
        LoadNewLevel(firstLevelToLoad);
    }

    public void OpenDoor()
    {
        if (enemyKilledInLevel >= enemyInLevel)
        {
            GameObject.Find("ExitDoor").GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
