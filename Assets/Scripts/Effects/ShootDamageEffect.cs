public class ShootDamageEffect:IDiceEffect {
    
    private int bonusDamage = 10;

    public void ActivateEffect()
    {
        PlayerStats.Instance.attackDamage += bonusDamage;
    }
}