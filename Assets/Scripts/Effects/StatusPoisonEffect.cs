public class StatusPoisonffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivatePoison();
    }

    public override string ToString()
    {
        return "<Poison Effect>";
    }
}