using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  
    private bool isPiercingBullet;
    private bool isBouncingBullet;
    private bool isFireBullet;
    private bool isPoisonBullet;
    private int damage;

    public void Awake(){
        isPiercingBullet = PlayerStats.Instance.isPiercingActivated;
        isBouncingBullet = PlayerStats.Instance.isBouncingActivated
        isFireBullet =  PlayerStats.Instance.isFireActivated;
        isPoisonBullet = PlayerStats.Instance.isPoisonActivated;
        damage = PlayerStats.Instance.attackDamage;;
    }

    public void OnEnemyCollision(Enemy enemy){
        enemy.isShot(damage,isFireBullet, isPoisonBullet);
        if(!isPiercingBullet) {
            Destroy(gameObject);
            //...
        }
        if(isBouncingBullet) {
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
    }
}
