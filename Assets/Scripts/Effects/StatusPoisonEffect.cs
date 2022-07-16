public class StatusPoisonffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivatePoison();
    }
    public string getTitle() {
        return "Poison bullets";
    }
    public string getHtmlText() {
        return "Shoot poisoned bullets, <b>kill ennemies</b> with a stacking dot !";
    }
    public override string ToString()
    {
        return "Poison Effect";
    }
}