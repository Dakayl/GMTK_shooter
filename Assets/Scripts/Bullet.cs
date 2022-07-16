using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static Color poisonColor = new Color(0.051f,1f,0f,1);
    private static Color fireColor = new Color(1f,0.235f,0f,1f);
    private static Color electricityColor = new Color(0f,0.8652f,1f,1f);
    private bool isPiercingBullet;
    private bool isBouncingBullet;
    private bool isExplodingBullet;
    private bool isDraculaBullet;
    private bool isFireBullet;
    private bool isPoisonBullet;
    private bool isElectricBullet;
    private float damage;
    private float lifeDuration = 2.9f;
    private float bounceForce = 1.0f;

     Color tintColor;

    public void Awake(){
        isPiercingBullet = PlayerStats.Instance.isPiercingActivated;
        isBouncingBullet = PlayerStats.Instance.isBouncingActivated;
        isExplodingBullet = PlayerStats.Instance.isExplodingActivated;
        isDraculaBullet = PlayerStats.Instance.isDraculaActivated;
        isFireBullet =  PlayerStats.Instance.isFireActivated;
        isPoisonBullet = PlayerStats.Instance.isPoisonActivated;
        isElectricBullet = PlayerStats.Instance.isElectricityActivated;
        damage = PlayerStats.Instance.attackDamage;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null) {
            if(isElectricBullet) {
                spriteRenderer.color = electricityColor;
            }
            if(isFireBullet) {
                spriteRenderer.color = fireColor;
            }
            if(isPoisonBullet) {
                spriteRenderer.color = poisonColor;
            }
        }
    }
    public void Update(){
        //checkRange
        lifeDuration -= Time.deltaTime;
        if(lifeDuration <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         Debug.Log(collision.gameObject.tag);
         if(collision.gameObject.tag == "Enemy")
        {
            OnEnemyCollision(collision.gameObject.GetComponent<Enemy>());
        } else if(collision.gameObject.tag == "MapWall"){

            OnWallCollision();
        }
    }

    public void OnEnemyCollision(Enemy enemy){
        enemy.isShot(damage,isFireBullet, isPoisonBullet, isElectricBullet);
        if(isDraculaBullet){
            float healBack = FightDraculaMode.percentageOfLife * damage;
            if(PlayerStats.Instance.currentHP < PlayerStats.Instance.maxHP) {
                PlayerStats.Instance.currentHP += healBack;
                if(PlayerStats.Instance.currentHP > PlayerStats.Instance.maxHP) {
                    PlayerStats.Instance.currentHP = PlayerStats.Instance.maxHP;
                }
            }
        }
        
       ManageCollision();
    }

    public void OnWallCollision(){
        ManageCollision();
    }

    public void ManageCollision() {
         if(isBouncingBullet) {
            lifeDuration += InteractionBouncingEffect.addedLifeDuration;
            isBouncingBullet = false;
            return;
        }
        if(isExplodingBullet) {
            //...
            Destroy(gameObject);
            return;
        }
        if(!isPiercingBullet) {
            Destroy(gameObject);
            return;
            //...
        }
        lifeDuration += InteractionPiercingEffect.addedLifeDuration;
        isPiercingBullet = false;
    }
}
