public class FightTwoBulletsMode:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateTwoBullets();
    }
    public string getTitle() {
        return "Fight Mode";
    }
    public string getHtmlText() {
        return "Two Bullets Mode: Shoot twice, <b>kill more</b> !";
    }
    public override string ToString()
    {
        return "Two Bullets Effect";
    }
}