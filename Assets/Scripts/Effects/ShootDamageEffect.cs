public class ShootDamageEffect:IDiceEffect {
    
    public int bonusDamage = 10;

    public void ActivateEffect()
    {
        Bullet.additionalDamage = bonusDamage;    
    }
}