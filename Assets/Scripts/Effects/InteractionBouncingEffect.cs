public class InteractionBouncingEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateBouncing();
    }
    public string getTitle() {
        return "Bouncing";
    }
    public string getHtmlText() {
        return "Your bullets <b>bounce</b> on wall and ennemies !";
    }
    public override string ToString()
    {
        return "Bouncing Effect";
    }
}