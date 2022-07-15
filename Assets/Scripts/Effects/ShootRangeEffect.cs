public class ShootRangeEffect:IDiceEffect {
    
    private int bonusRange = 10;

    public void ActivateEffect()
    {
         PlayerStats.Instance.attackRange += bonusRange;
    }

    public override string ToString()
    {
        return "<ShootRange Effect>";
    }
}