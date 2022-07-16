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
    }

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

        TakeDamage(damage);
    }


    public void TakeDamage(float ammount)
    {
        currentLifePoints -= ammount;
        if(currentLifePoints <= 0)
        {
            Kill();
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
            Debug.Log(rectTransform.sizeDelta);
            rectTransform.sizeDelta = new Vector2 (ratio, 0.14f);
        }
    }

}
