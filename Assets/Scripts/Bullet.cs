using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private bool isPiercingBullet;
    private bool isBouncingBullet;
    private bool isExplodingBullet;
    private bool isDraculaBullet;
    private bool isFireBullet;
    private bool isPoisonBullet;
    private bool isElectricBullet;
    private float damage;

    public void Awake(){
        isPiercingBullet = PlayerStats.Instance.isPiercingActivated;
        isBouncingBullet = PlayerStats.Instance.isBouncingActivated;
        isExplodingBullet = PlayerStats.Instance.isExplodingActivated;
        isDraculaBullet = PlayerStats.Instance.isDraculaActivated;
        isFireBullet =  PlayerStats.Instance.isFireActivated;
        isPoisonBullet = PlayerStats.Instance.isPoisonActivated;
        isElectricBullet = PlayerStats.Instance.isElectricityActivated;
        damage = PlayerStats.Instance.attackDamage;
    }

    public void OnEnemyCollision(Enemy enemy){
        enemy.isShot(damage,isFireBullet, isPoisonBullet, isElectricBullet);
        if(isDraculaBullet){

        }
        if(!isPiercingBullet) {
            Destroy(gameObject);
            //...
        }
        if(isBouncingBullet) {
            //...
        }
        if(isExplodingBullet) {
            //...
        }
    }

     public void OnWallCollision(){
        if(!isPiercingBullet) {
            Destroy(gameObject);
            //...
        }
        if(isBouncingBullet) {
            //...
        }
        if(isExplodingBullet) {
            //...
        }
    }
}
