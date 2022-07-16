using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject deathParticlesPrefab;

    [SerializeField] private GameObject greenbar;
    private bool isOnFire = false;
    private bool isOnPoison = false;
    private int poisonStack = 0;
    private bool isOnElectric = false;
    private float currentFireDuration = 0;
    private float currentElectricDuration = 0;
    public static float baselifePoints = 100;
    private float currentLifePoints = 100;
    private float baseLifePoints = 100;

    public void Awake() {
        currentLifePoints = baselifePoints;
        isShot(1,true, false, true);
        

    }

    public void isShot(float damage, bool isFire, bool isPoison, bool isElectric) {
        if(isFire) {
            if(!isOnFire) {
                InvokeRepeating("FireTick", StatusFireEffect.dotTickDuration, StatusFireEffect.dotTickDuration);
            }
            isOnFire = true; //TO DO real effect
            currentFireDuration = StatusFireEffect.fullDuration;
        }
        if(isPoison) {
            if(!isOnPoison) {
                InvokeRepeating("PoisonTick", StatusPoisonEffect.dotTickDuration, StatusPoisonEffect.dotTickDuration);
            }
            isOnPoison = true; //TO DO real effect
            poisonStack += StatusPoisonEffect.stackNumber;
        }
         if(isElectric) {
            Stun();
            Invoke("EletricTick", StatusElectricityEffect.duration);
            isOnElectric = true; //TO DO real effect
            currentElectricDuration = StatusElectricityEffect.duration;
        }

        TakeDamage(damage);
    }

    public void Stun(){
         // EletricParticles ?
    }

    public void Unstun(){
         // EletricParticles ?
    }


    public void TakeDamage(float amount)
    {
        currentLifePoints -= amount;
        if(currentLifePoints <= 0)
        {
            Kill();
        }
    }

    public void PoisonTick() {
        TakeDamage(poisonStack * StatusPoisonEffect.damagePerStack);
      
        // poisonParticles ?
    }

    public void FireTick() {
        TakeDamage( StatusFireEffect.fireDamage);
        currentFireDuration -= StatusFireEffect.dotTickDuration;
        if(currentFireDuration <= 0) {
            isOnFire = false;
        }
        // FireParticles ?
    }

    public void EletricTick() {
        currentElectricDuration -= StatusElectricityEffect.duration;
        if(currentElectricDuration <= 0) {
            isOnElectric = false;
            Unstun();
            isShot(1,true, false, false);
        }       
    }

    public void Kill()
    {
        GameObject myParticles = GameObject.Instantiate(deathParticlesPrefab, transform.position, new Quaternion());
        myParticles.GetComponent<ParticleSystem>().Emit(20);

        Destroy(gameObject);
    }
    public void Update()
    {
        //TO DO replace with proper event mgt
        float ratio = currentLifePoints / baselifePoints;
        RectTransform rectTransform = greenbar.GetComponent<RectTransform>();
        if(rectTransform) {
            rectTransform.sizeDelta = new Vector2 (ratio, 0.14f);
        }
    }

}
