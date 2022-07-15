public class InteractionPiercingEffect:IDiceEffect {
    
    public void ActivateEffect()
    {
        PlayerStats.Instance.ActivatePiercing();
    }

    public override string ToString()
    {
        return "<Piercing Effect>";
    }
}