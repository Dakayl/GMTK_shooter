public class InteractionBouncingEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivateBouncing();
    }
}