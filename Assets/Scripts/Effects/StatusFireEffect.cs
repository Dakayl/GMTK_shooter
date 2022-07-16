public class StatusFireEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateFire(); 
    }
    public string getTitle() {
        return "Fire bullets";
    }
    public string getHtmlText() {
        return "Shoot fire bullets, <b>burn ennemies</b> with a lasting dot !";
    }
    public override string ToString()
    {
        return "Fire Effect";
    }
}