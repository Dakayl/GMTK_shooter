using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private bool isOnFire = false;
    private bool isOnPoison = false;
    private int lifePoints = 100;

    public isShotBy(Bullet bullet){
        if(bullet.isFire) {
            isOnFire = true;
        }
        if(bullet.isPoison) {
            isOnPoison = true;
        }
        lifePoints -= bullet.damage;
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
