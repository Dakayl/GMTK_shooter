public class StatusFireEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateFire(); 
    }

    public override string ToString()
    {
        return "<Fire Effect>";
    }
}