public class InteractionPiercingEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
       PlayerStats.Instance.ActivatePiercing();
    }
}