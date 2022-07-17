using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectToGameManager : MonoBehaviour
{
    public void LoadNewLevel()
    {
        GameManager.Instance.LoadNewLevel();
    }

    public void GoToNextFloor()
    {
        GameManager.Instance.GoToNextFloor();
    }

    public void NewEnemyKilled()
    {
        GameManager.Instance.NewEnemyKilled();
    }

    public void NewDiceLaunch()
    {
        GameManager.Instance.NewDiceLaunch();
    }
}
