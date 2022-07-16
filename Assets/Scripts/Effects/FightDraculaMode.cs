public class FightDraculaMode:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateDracula();
    }
    public string getTitle() {
        return "Dracula Bullets";
    }
    public string getHtmlText() {
        return "Shoot and regain health";
    }
    public override string ToString()
    {
        return "Dracula Effect";
    }
}