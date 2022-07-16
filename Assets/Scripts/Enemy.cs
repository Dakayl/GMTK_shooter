using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private bool isOnFire = false;
    private bool isOnPoison = false;
    private int poisonStack = 0;
    private bool isOnElectric = false;
    private float currentFireDuration = 0;
    private float currentElectricDuration = 0;
    private float lifePoints = 100;

    public void isShot(float damage, bool isFire, bool isPoison, bool isElectric) {
        if(isFire) {
            isOnFire = true; //TO DO real effect
            currentFireDuration = StatusFireEffect.fullDuration;
        }
        if(isPoison) {
            isOnPoison = true; //TO DO real effect
            poisonStack += StatusPoisonffect.stackNumber;
        }
         if(isElectric) {
            isOnElectric = true; //TO DO real effect
            currentElectricDuration = StatusElectricityEffect.duration;
        }
        lifePoints -= damage;
    }

}
