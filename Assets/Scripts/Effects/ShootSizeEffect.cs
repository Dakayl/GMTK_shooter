public class ShootSizeEffect:IDiceEffect {
    
    private float bonusSize = 0.5f; //ratio

    public void ActivateEffect()
    {
        PlayerStats.Instance.bulletSize += bonusSize;
    }
    public string getTitle() {
        return "Shoot Size";
    }
    public string getHtmlText() {
        return "Your bullets are  <b>bigger</b> !";
    }
    public override string ToString()
    {
        return "Shoot Size Effect";
    }
}