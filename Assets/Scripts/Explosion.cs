using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public static float explosionDamage = 5.0f;
    public static float explosionDuration = 0.7f;

    public void Awake(){
        Invoke(methodName: "Remove", explosionDuration);
    }

    public void Remove(){
        Destroy(gameObject);
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject.tag == "Enemy")
        {
           Enemy enemy = collision.gameObject.GetComponent<Enemy>();
           enemy.isShot(explosionDamage);
        }
    }
}
