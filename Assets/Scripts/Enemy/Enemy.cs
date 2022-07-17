using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathParticlesPrefab;
    [SerializeField] private GameObject fireParticlesPrefab;
    [SerializeField] private GameObject thunderParticlesPrefab;
    [SerializeField] private GameObject poisonParticlesPrefab;

    [SerializeField] private GameObject greenbar;
    private bool isOnFire = false;
    private bool isOnPoison = false;
    private int poisonStack = 0;
    private float currentFireDuration = 0;
    private float currentElectricDuration = 0;
    public static float baselifePoints = 95;
    public static float lifePointsPerLevel = 5;
    private float currentLifePoints = 100;
    private float baseAttackDamage = 18;
    public static float attackPerLevel = 2;
    [HideInInspector] public float currentAttackDamage = 20;
    private EnemyMovement myMovement;
   
    public void Awake() {
        myMovement = GetComponent<EnemyMovement>();
    }

    public void Start() {
        currentLifePoints = baselifePoints + GameManager.Instance.currentLevel * lifePointsPerLevel;
        currentAttackDamage = baseAttackDamage + GameManager.Instance.currentLevel * attackPerLevel;
    }

    public void isShot(float damage, bool isFire = false, bool isPoison = false, bool isElectric = false) {
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
            currentElectricDuration = StatusElectricityEffect.duration;
        }

        TakeDamage(damage);
    }

    public void Stun(){
        myMovement.isStunned = true;
        GameObject myParticles = GameObject.Instantiate(thunderParticlesPrefab, transform.position, new Quaternion());
        myParticles.GetComponent<ParticleSystem>().Emit(20);
    }

    public void Unstun(){
        myMovement.isStunned = false;
        GameObject myParticles = GameObject.Instantiate(thunderParticlesPrefab, transform.position, new Quaternion());
        myParticles.GetComponent<ParticleSystem>().Emit(20);
    }


    public void TakeDamage(float amount)
    {
        currentLifePoints -= amount;
        if(currentLifePoints <= 0)
        {
            Kill();
        }
        if(amount >= currentLifePoints/10)
            myMovement.TookDamage();
    }

    public void PoisonTick() {
        TakeDamage(poisonStack * StatusPoisonEffect.damagePerStack);
        GameObject myParticles = GameObject.Instantiate(poisonParticlesPrefab, transform.position, new Quaternion());
        myParticles.GetComponent<ParticleSystem>().Emit(20);
    }

    public void FireTick() {
        TakeDamage( StatusFireEffect.fireDamage);
        currentFireDuration -= StatusFireEffect.dotTickDuration;
        if(currentFireDuration <= 0) {
            isOnFire = false;
            CancelInvoke("FireTick");
        }
        GameObject myParticles = GameObject.Instantiate(fireParticlesPrefab, transform.position, new Quaternion());
        myParticles.GetComponent<ParticleSystem>().Emit(20);
    }

    public void EletricTick() {
        currentElectricDuration -= StatusElectricityEffect.duration;
        if(currentElectricDuration <= 0) {
            Unstun();
        }       
    }

    public void Kill()
    {
        GameObject myParticles = GameObject.Instantiate(deathParticlesPrefab, transform.position, new Quaternion());
        myParticles.GetComponent<ParticleSystem>().Emit(20);
        Destroy(gameObject);
        GameManager.Instance.NewEnemyKilled();
    }
    public void Update()
    {
        //TO DO replace with proper event mgt
        float ratio = currentLifePoints / (baselifePoints + GameManager.Instance.currentLevel * lifePointsPerLevel);
        RectTransform rectTransform = greenbar.GetComponent<RectTransform>();
        if(ratio < 0.07f) ratio = 0.07f;
        if(rectTransform) {
            rectTransform.sizeDelta = new Vector2 (ratio, 0.14f);
        }
    }

    public void DamagePlayer()
    {
        PlayerStats.Instance.TakeDamage(currentAttackDamage);
    }

}
