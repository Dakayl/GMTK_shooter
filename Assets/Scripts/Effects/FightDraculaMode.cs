public class FightDraculaMode:IDiceEffect {
    
    public static float percentageOfLife = 0.4f;
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateDracula();
    }
    public string getTitle() {
        return "Fight Mode";
    }
    public string getHtmlText() {
        return string.Format("Leech : Shoot and regain {0}% of damages as health.", percentageOfLife*100);
    }
    public override string ToString()
    {
        return "Dracula Effect";
    }
}