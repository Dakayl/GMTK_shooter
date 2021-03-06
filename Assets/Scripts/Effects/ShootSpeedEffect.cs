public class ShootSpeedEffect:IDiceEffect {
    
    private static float bonusSpeed = 1.5f;

    public void ActivateEffect()
    {
        PlayerStats.Instance.attackSpeed += bonusSpeed;
    }
    public string getTitle() {
        return "Projectile Upgrade";
    }
    public string getHtmlText() {
        return "Shoot bullets <b>faster</b> !";
    }
    public override string ToString()
    {
        return "Shoot Speed Effect";
    }
}