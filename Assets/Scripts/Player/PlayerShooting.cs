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
    [SerializeField] private AudioClip[] shootAudio;
    private float deviationAngle = 0.15f;

    private bool isCoolingDown = false;
    private float coolDownTimer = 0f;
    private bool canShoot = true;

     public void playRandomFromArray(AudioClip[] listClips, float volume){
        if(listClips.Length < 1) return;
        int index = Random.Range(0, listClips.Length);
        if(listClips[index] != null) {
            AudioClip clip = listClips[index];
            SoundPlayer.Play(clip, volume);
        }
    }

    private void Update()
    {
        if (canShoot)
        {
            if (Input.GetButton("Fire1"))
            {
                if(Time.timeScale == 0) return;
                if(isCoolingDown == false)
                {
                    if(PlayerStats.Instance.isTwoBulletsActivated) {
                        Shoot(-deviationAngle);
                        Shoot(+deviationAngle);
                        playRandomFromArray(shootAudio, 0.6f);
                    } else {
                        Shoot();  
                        playRandomFromArray(shootAudio, 0.4f);
                    }
                    isCoolingDown = true;
                }
            }
        }
        if (isCoolingDown)  // Attack cooldown
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
        CinemachineShake.Instance.ShakeCamera(0.5f,0.1f);

        GameObject bullet = Instantiate(projectilePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bulletSpawnPoint.right * bulletForce, ForceMode2D.Impulse);
        Vector2 scale = bullet.transform.localScale;
        if(PlayerStats.Instance.bulletSize > PlayerStats.Instance.baseBulletSize) {
            bullet.transform.localScale *= PlayerStats.Instance.bulletSize;;
        }
    }

    public void setShooting(bool status)
    {
        canShoot = status;
    }
}
