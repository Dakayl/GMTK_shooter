public class InteractionBouncingEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateBouncing();
    }

    public override string ToString()
    {
        return "<Bouncing Effect>";
    }
}