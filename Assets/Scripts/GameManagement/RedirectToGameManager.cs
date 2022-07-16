using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectToGameManager : MonoBehaviour
{
    public void LoadNewLevel()
    {
        GameManager.Instance.LoadNewLevel();
    }

    public void LoadNewLevel(int value)
    {
        GameManager.Instance.LoadNewLevel(value);
    }

    public void GoToNextFloor()
    {
        GameManager.Instance.GoToNextFloor();
    }

    public void NewEnemyKilled()
    {
        GameManager.Instance.NewEnemyKilled();
    }
}
