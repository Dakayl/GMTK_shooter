public class StatusElectricityEffect:IDiceEffect {
    
    public static float duration  = 0.5f;
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateElectricity();
    }
    public string getTitle() {
        return "Electric bullets";
    }
    public string getHtmlText() {
        return string.Format("Stun the target for <b>{0}</b> sec per hit. Duration is stackable", duration);
    }
    public override string ToString()
    {
        return "Electricity Effect";
    }
}