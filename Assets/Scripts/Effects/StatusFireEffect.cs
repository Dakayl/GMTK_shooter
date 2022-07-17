public class StatusFireEffect:IDiceEffect {
    
    public static int fireDamage = 4;
    public static float dotTickDuration  = 0.5f;
    public static float fullDuration  = 4f;
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateFire(); 
    }
    public string getTitle() {
        return "Damage Type";
    }
    public string getHtmlText() {
        return string.Format("Deal <b>{0}</b> fire damage each {1} sec, during {2} sec. Duration is stackable.", fireDamage, dotTickDuration, fullDuration);
    }
    public override string ToString()
    {
        return "Fire Effect";
    }
}