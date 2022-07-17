public class StatusPoisonEffect:IDiceEffect {
    
    public static int stackNumber = 1;
    public static int damagePerStack = 1;
    public static float dotTickDuration  = 2;
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivatePoison();
    }
    public string getTitle() {
        return "Damage Type";
    }
    public string getHtmlText() {
        return string.Format("Add <b>{0} poison stack</b> per hit, stack apply damage every <b>{1} sec</b>.", stackNumber, dotTickDuration);
    }
    public override string ToString()
    {
        return "Poison Effect";
    }
}