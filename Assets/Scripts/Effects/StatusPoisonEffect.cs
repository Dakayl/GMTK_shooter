public class StatusPoisonffect:IDiceEffect {
    
    public static int stackNumber = 1;
    public static float dotTickDuration  = 2;
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivatePoison();
    }
    public string getTitle() {
        return "Poison bullets";
    }
    public string getHtmlText() {
        return string.Format("Stack <b>{0} poison</b> per hit, damages apply every <b>{1} sec</b>", stackNumber, dotTickDuration);
    }
    public override string ToString()
    {
        return "Poison Effect";
    }
}