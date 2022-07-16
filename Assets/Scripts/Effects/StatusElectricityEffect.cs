public class StatusElectricityEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateElectricity();
    }
    public string getTitle() {
        return "Electric bullets";
    }
    public string getHtmlText() {
        return "Shoot electric bullets, <b>stun ennemies</b> !";
    }
    public override string ToString()
    {
        return "Electricity Effect";
    }
}