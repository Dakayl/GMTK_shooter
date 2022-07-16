public class FightArmorMode:IDiceEffect {
    
    public static float bonusArmor = 3;

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