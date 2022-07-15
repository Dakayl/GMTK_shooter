public class StatusPoisonffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivatePoison();
    }
}