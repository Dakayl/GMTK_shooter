using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float bulletForce = 2f;
    private float deviationAngle = 0.15f;

    private bool isCoolingDown = false;
    private float coolDownTimer = 0f;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if(isCoolingDown == false)
            {
                if(PlayerStats.Instance.isTwoBulletsActivated) {
                    Shoot(-deviationAngle);
                    Shoot(+deviationAngle);
                } else {
                    Shoot();  
                }
                isCoolingDown = true;
            }
            
            
        } else if (isCoolingDown)  // Attack cooldown
        {
            coolDownTimer += Time.deltaTime;
            if(coolDownTimer >= 1/ PlayerStats.Instance.attackSpeed)
            {
                isCoolingDown = false;
                coolDownTimer = 0;
            }
        }
    }


    private void Shoot(float angle = 0)
    {
        GameObject bullet = Instantiate(projectilePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bulletSpawnPoint.right * bulletForce, ForceMode2D.Impulse);
        Vector2 scale = bullet.transform.localScale;
        if(PlayerStats.Instance.bulletSize > PlayerStats.Instance.baseBulletSize) {
            bullet.transform.localScale *= PlayerStats.Instance.bulletSize;;
        }
    }
}
