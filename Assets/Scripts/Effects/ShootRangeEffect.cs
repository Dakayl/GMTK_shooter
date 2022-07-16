public class ShootRangeEffect:IDiceEffect {
    
    private float bonusRange = 25;

    public void ActivateEffect()
    {
        PlayerStats.Instance.attackRange += bonusRange;
    }
    public string getTitle() {
        return "Shoot Range";
    }
    public string getHtmlText() {
        return "Your bullets go <b>further</b> !";
    }
    public override string ToString()
    {
        return "Shoot Range Effect";
    }
}