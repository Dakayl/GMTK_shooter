public class ShootSpeedEffect:IDiceEffect {
    
    private float bonusSpeed = 10;

    public void ActivateEffect()
    {
        PlayerStats.Instance.attackSpeed += bonusSpeed;
    }
    public string getTitle() {
        return "Shoot Speed";
    }
    public string getHtmlText() {
        return "Shoot bullets <b>faster</b> !";
    }
    public override string ToString()
    {
        return "Shoot Speed Effect";
    }
}