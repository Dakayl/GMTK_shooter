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
    private float deviationAngle = 0.15f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(PlayerStats.Instance.isTwoBulletsActivated);
            if(PlayerStats.Instance.isTwoBulletsActivated) {
                Shoot(-deviationAngle);
                Shoot(+deviationAngle);
            } else {
                Shoot();
            }
        }
    }


    private void Shoot(float angle = 0)
    {
        Debug.Log(angle);
        Quaternion rotation = bulletSpawnPoint.rotation;
        rotation.z += angle;
        GameObject bullet = Instantiate(projectilePrefab, bulletSpawnPoint.position, rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bulletSpawnPoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
