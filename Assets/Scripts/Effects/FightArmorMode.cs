public class FightArmorMode:IDiceEffect {
    
    public static int bonusArmor = 25;

    public void ActivateEffect()
    {
        PlayerStats.Instance.armor += bonusArmor;
    }
    public string getTitle() {
        return "Fight Mode";
    }
    public string getHtmlText() {
        return "Armor mode : Receive less damages ";
    }
    public override string ToString()
    {
        return "Armor Effect";
    }
}