using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; } // Singleton


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple CinemachineShake Error");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShakeCamera(float intensity, float duration)
    {
        CinemachineImpulseSource mySource = gameObject.GetComponent<CinemachineImpulseSource>();
        mySource.GenerateImpulse(intensity * 0.15f);
    }
}
