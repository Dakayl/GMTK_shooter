public class InteractionPiercingEffect:IDiceEffect {
    
    public static float addedLifeDuration = 1.5f;
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivatePiercing();
    }
    public string getTitle() {
        return "Impact reaction";
    }
    public string getHtmlText() {
        return "Your bullets <b>pierce</b> once walls and ennemies !";
    }
    public override string ToString()
    {
        return "Piercing Effect";
    }
}