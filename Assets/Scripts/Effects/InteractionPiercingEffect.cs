public class InteractionPiercingEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivatePiercing();
    }
    public string getTitle() {
        return "Piercing";
    }
    public string getHtmlText() {
        return "Your bullets <b>pierce</b> walls and ennemies !";
    }
    public override string ToString()
    {
        return "Piercing Effect";
    }
}