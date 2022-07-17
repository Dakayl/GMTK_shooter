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
        return string.Format("Your bullets are {0}% <b>bigger</b> !", bonusSize*100);
    }
    public override string ToString()
    {
        return "Shoot Size Effect";
    }
}