public class FightArmorMode:IDiceEffect {
    
    private int bonusArmor = 25;

    public void ActivateEffect()
    {
        PlayerStats.Instance.armor += bonusArmor;
    }
    public string getTitle() {
        return "Armor";
    }
    public string getHtmlText() {
        return "Receive less damages ";
    }
    public override string ToString()
    {
        return "Armor Effect";
    }
}