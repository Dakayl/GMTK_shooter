using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private bool isOnFire = false;
    private bool isOnPoison = false;
    private int lifePoints = 100;

    public isShot(int damage, bool isFire, bool isPoison) {
        if(isFire) {
            isOnFire = true;
        }
        if(isPoison) {
            isOnPoison = true;
        }
        lifePoints -= bullet.damage;
    }

}
