public class InteractionBouncingEffect:IDiceEffect {
    
    public static float addedLifeDuration = 1.5f;
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateBouncing();
    }
    public string getTitle() {
        return "Impact reaction";
    }
    public string getHtmlText() {
        return "Your bullets <b>bounce</b> once on walls and ennemies !";
    }
    public override string ToString()
    {
        return "Bouncing Effect";
    }
}