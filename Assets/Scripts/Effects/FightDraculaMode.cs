public class FightDraculaMode:IDiceEffect {
    
    public static float percentageOfLife = 0.5f;
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateDracula();
    }
    public string getTitle() {
        return "Fight Mode";
    }
    public string getHtmlText() {
        return "Dracula mode : Shoot and regain health";
    }
    public override string ToString()
    {
        return "Dracula Effect";
    }
}