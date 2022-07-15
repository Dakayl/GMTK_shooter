public class ShootSpeedEffect:IDiceEffect {
    
    private int bonusSpeed = 10;

    public void ActivateEffect()
    {
        PlayerStats.Instance.attackSpeed += bonusSpeed;
    }
}