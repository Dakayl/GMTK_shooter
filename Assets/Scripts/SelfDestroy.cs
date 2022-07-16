using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField]
    private float selfDestroyTime = 2;

    private float selfDestroyTimer = 0;

    void Update()
    {
        selfDestroyTimer += Time.deltaTime;

        if(selfDestroyTimer >= selfDestroyTime)
        {
            Destroy(gameObject);
        }
    }
}
