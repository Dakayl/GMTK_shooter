public class ShootSizeEffect:IDiceEffect {
    
    private static float bonusSize = 0.5f; //ratio

    public void ActivateEffect()
    {
        PlayerStats.Instance.bulletSize += bonusSize;
    }
    public string getTitle() {
        return "Projectile Upgrade";
    }
    public string getHtmlText() {
        return "Your bullets are  <b>bigger</b> !";
    }
    public override string ToString()
    {
        return "Shoot Size Effect";
    }
}