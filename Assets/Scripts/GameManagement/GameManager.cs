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

    [SerializeField]
    private AudioClip[] musics;

    [SerializeField] private AudioClip[] steps;
    [SerializeField] private AudioClip[] looseJingle;

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
    }
    private void Start()
    {
        for (int i = 0; i < dontDestroyOnLoad.Length; i++)
        {
            //Debug.Log(dontDestroyOnLoad[i]);
            DontDestroyOnLoad(dontDestroyOnLoad[i]);
        }
        LoadNewLevel(firstLevelToLoad);
        UIAnim.Play("WelcomeUI");
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                MusicPlayer.Instance.Stop();
                playRandomFromArray(looseJingle, 0.8f);
                Debug.Log("Trying my best");
                UIAnim.Play("Reset");
                
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            playRandomFromArray(steps, 0.8f);
            UIAnim.Play("ScreenTransition");
           
        }
    }

    public void playRandomFromArray(AudioClip[] listClips, float volume = 0.7f){
        if(listClips.Length < 1) return;
        int index = Random.Range(0, listClips.Length);
        if(listClips[index] != null) {
            AudioClip clip = listClips[index];
            SoundPlayer.Play(clip, volume);
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
        int nextLevelIndex;
        if (currentLevel <= 3)
        {
            nextLevelIndex = lastLevelIndex + 1;
        }
        else
        {
            nextLevelIndex = Random.Range(4,8);
        }
        SceneManager.LoadScene(nextLevelIndex);
        if(currentLevel == 1)
        {
            playerStats.gameObject.transform.position = new Vector3(-4, 0, -1);
        }
        else
        {
            playerStats.gameObject.transform.position = new Vector3(-7.5f, -1.5f, -1);
        }
        enemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyKilledInLevel = 0;
        lastLevelIndex = nextLevelIndex;
        PlayMusic();
    }
    public void LoadNewLevel(int levelIndex)
    {
        currentLevel++;
        SceneManager.LoadScene(levelIndex);
        if (currentLevel == 1)
        {
            playerStats.gameObject.transform.position = new Vector3(-4, 0, -1);
        }
        else
        {
            playerStats.gameObject.transform.position = new Vector3(-7.5f, -1.5f, -1);
        }
        enemyInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyKilledInLevel = 0;
        lastLevelIndex = levelIndex;
        PlayMusic();
    }

    public void PlayMusic() {
        switch(currentLevel) {
            case 1: 
                MusicPlayer.Instance.Play(musics[0]); break;
            case 2: 
                MusicPlayer.Instance.Play(musics[1]); break;
            case 3: 
                MusicPlayer.Instance.Play(musics[2]); break;
            case 4: 
                MusicPlayer.Instance.Play(musics[3]); break;
            default: 
                MusicPlayer.Instance.Play(musics[4]); break;
        }
    }

    public void GoToNextFloor()
    {
        UIAnim.Play("ScreenTransition");
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
        DiceRack.Instance.ResetDice();
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
