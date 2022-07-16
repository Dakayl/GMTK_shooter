public class InteractionExplosionEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateExploding();
    }
    public string getTitle() {
        return "Impact reaction";
    }
    public string getHtmlText() {
        return "Your bullets <b>explode</b> on wall and ennemies, dealing AOE damages !";
    }
    public override string ToString()
    {
        return "Exploding Effect";
    }
}