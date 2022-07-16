public class FightTwoBulletsMode:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateTwoBullets();
    }
    public string getTitle() {
        return "Two Bullets";
    }
    public string getHtmlText() {
        return "Shoot twice, <b>kill more</b> !";
    }
    public override string ToString()
    {
        return "Two Bullets Effect";
    }
}