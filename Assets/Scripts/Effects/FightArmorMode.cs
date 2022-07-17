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
        return string.Format("Armor mode : Receive no damages <b>{0}</b> times.", bonusArmor);
    }
    public override string ToString()
    {
        return "Armor Effect";
    }
}