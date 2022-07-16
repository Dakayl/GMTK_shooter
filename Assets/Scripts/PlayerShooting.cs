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
    private float bulletForce = 20f;

    private bool isCoolingDown = false;
    private float coolDownTimer = 0f;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }

        // Attack cooldown
        if (isCoolingDown)
        {
            coolDownTimer += Time.deltaTime;
            if(coolDownTimer >= 1/ PlayerStats.Instance.attackSpeed)
            {
                isCoolingDown = false;
                coolDownTimer = 0;
            }
        }
    }


    private void Shoot()
    {
        if(isCoolingDown == false)
        {
            GameObject bullet = Instantiate(projectilePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(bulletSpawnPoint.right * bulletForce, ForceMode2D.Impulse);

            isCoolingDown = true;
        }
    }
}
