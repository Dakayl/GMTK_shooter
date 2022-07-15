using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static bool isPiercingMode = false;
    public static bool isBouncingMode = false;
    public static bool isFireMode = false;
    public static bool isPoisonMode = false;

    public static int additionalDamage = 0;

    public static reset() {
        isPiercingMode = false;
        isBouncingMode = false;
        isFireMode = false;
        isPoisonMode = false;
        additionalDamage = 0;
    }

    private bool isPiercingBullet;
    private bool isBouncingBullet;
    private bool isFireBullet;
    private bool isPoisonBullet;

    private int basicDamage = 10;
    private int currentDamage = 0;

    public int damage {
        get{
            return currentDamage;
        }
    }

    public bool isFire {
        get{
            return isFireBullet;
        }
    }
    
    public bool isPoison {
        get{
            return isPoisonMode;
        }
    }
    
    public void Awake(){
        isPiercingBullet = static.isPiercingMode;
        isBouncingBullet = static.isBouncingMode;
        isFireBullet = static.isFireMode;
        isPoisonBullet = static.isPoisonMode;
        currentDamage = basicDamage + additionalDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
