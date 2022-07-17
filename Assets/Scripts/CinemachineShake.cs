using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; } // Singleton

    private CinemachineVirtualCamera cinemachineVC;
    private float shakeTimer;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple CinemachineShake Error");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBMCP =
                    cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBMCP.m_AmplitudeGain = 0;

            }
        }
    }

    public void ShakeCamera(float intensity, float duration)
    {

        CinemachineBasicMultiChannelPerlin cinemachineBMCP =
            cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBMCP.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }
}
