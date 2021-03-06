using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject destroyParticlePrefab;
    [SerializeField]
    private bool isPlayerBullet = true;
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
    [HideInInspector] public float damage;
    [SerializeField] private AudioClip[] wallAudio;
    private float lifeDuration = 2.9f;
    

     Color tintColor;

    public void Awake(){
        if (isPlayerBullet)
        {
            isPiercingBullet = PlayerStats.Instance.isPiercingActivated;
            isBouncingBullet = PlayerStats.Instance.isBouncingActivated;
            isExplodingBullet = PlayerStats.Instance.isExplodingActivated;
            isDraculaBullet = PlayerStats.Instance.isDraculaActivated;
            isFireBullet =  PlayerStats.Instance.isFireActivated;
            isPoisonBullet = PlayerStats.Instance.isPoisonActivated;
            isElectricBullet = PlayerStats.Instance.isElectricityActivated;
            damage = PlayerStats.Instance.attackDamage;
        }
        else
        {
            isPiercingBullet = false;
            isBouncingBullet = false;
            isExplodingBullet = false;
            isDraculaBullet = false;
            isFireBullet = false;
            isPoisonBullet = false;
            isElectricBullet = false;
            damage = 3;
        }
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

    public void playRandomFromArray(AudioClip[] listClips, float volume){
        if(listClips.Length < 1) return;
        int index = Random.Range(0, listClips.Length);
        if(listClips[index] != null) {
            AudioClip clip = listClips[index];
            SoundPlayer.Play(clip, volume);
        }
    }

    public void Update(){
        //checkRange
        lifeDuration -= Time.deltaTime;
        if(lifeDuration <= 0) {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject.tag == "Enemy" && isPlayerBullet)
        {
            OnEnemyCollision(collision.gameObject.GetComponent<Enemy>());
            ManageCollision(collision);
        }
        else if(collision.gameObject.tag == "MapWall"){

            OnWallCollision(); 
            ManageCollision(collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isPlayerBullet)
        {
            OnPlayerCollision(collision.gameObject.GetComponent<PlayerStats>());
            lifeDuration = 0;
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
        
      
    }

    public void OnPlayerCollision(PlayerStats pStats)
    {
        pStats.TakeDamage(damage);
    }

    public void OnWallCollision(){
       playRandomFromArray(wallAudio, 0.6f);
    }

    public void Bounce(Collider2D collision){
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb == null) return;
        Vector2 lastVelocity = rb.velocity;
        Vector2 towardsCollision = collision.transform.position - transform.position;
        Ray2D ray = new Ray2D(transform.position, towardsCollision);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1f);
        if(hit.transform != null)
        {
            Vector2 surfaceNormal = hit.normal;
            Vector2 newVelocity = Vector2.Reflect(lastVelocity, surfaceNormal);;
            rb.velocity = newVelocity;
            rb.MoveRotation(Quaternion.LookRotation(newVelocity.normalized));
        }
    }

    public void Explode() {
         GameObject explosion = Instantiate(explosionPrefab );
         explosion.transform.position = transform.position;
    }

    public void ManageCollision(Collider2D collision) {
         if(isBouncingBullet) {
            Bounce(collision);
            isBouncingBullet = false;
            return;
        }
        if(isExplodingBullet) {
            Explode();
            DestroyBullet();
            return;
        }
        if(!isPiercingBullet) {
            DestroyBullet();
            return;
        }
        lifeDuration += InteractionPiercingEffect.addedLifeDuration;
        isPiercingBullet = false;
    }

    private void DestroyBullet()
    {
        GameObject particles = Instantiate(destroyParticlePrefab, transform.position, new Quaternion());
        particles.GetComponent<ParticleSystem>().Emit(10);
        Destroy(gameObject);
    }
}
