using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private bool isOnFire = false;
    private bool isOnPoison = false;
    private float lifePoints = 100;

    public void isShot(float damage, bool isFire, bool isPoison) {
        if(isFire) {
            isOnFire = true;
        }
        if(isPoison) {
            isOnPoison = true;
        }
        lifePoints -= damage;
    }

}
