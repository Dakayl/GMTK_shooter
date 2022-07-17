using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct DiceFace {
    public IDiceEffect effect;
    
    public DiceFace(IDiceEffect effect) {
        this.effect = effect;
    }
}

public class Dice
{
    private List<DiceFace> faceList;
    private DiceFace currentFace;
    public DiceFace face { get { return currentFace; }}
    public int diceType;

    public void AddFace(DiceFace face){
        faceList.Add(face);
    }

    public void CreateInteractionDice() {
        diceType = 1;
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new InteractionBouncingEffect()));
        AddFace (new DiceFace(new InteractionBouncingEffect()));
        AddFace (new DiceFace(new InteractionExplosionEffect()));
        AddFace (new DiceFace(new InteractionExplosionEffect()));
        AddFace (new DiceFace(new InteractionPiercingEffect()));
        AddFace (new DiceFace(new InteractionPiercingEffect()));
        currentFace = faceList[0];
    }

    public void CreateShootDice() {
        diceType = 2;
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new ShootSizeEffect()));
        AddFace (new DiceFace(new ShootSizeEffect()));
        AddFace (new DiceFace(new ShootRangeEffect()));
        AddFace (new DiceFace(new ShootRangeEffect()));
        AddFace (new DiceFace(new ShootSpeedEffect()));
        AddFace (new DiceFace(new ShootSpeedEffect()));
        currentFace = faceList[0];
    }

    public void CreateStatusDice() {
        diceType = 3;
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new StatusFireEffect()));
        AddFace (new DiceFace(new StatusFireEffect()));
        AddFace (new DiceFace(new StatusElectricityEffect()));
        AddFace (new DiceFace(new StatusElectricityEffect()));
        AddFace (new DiceFace(new StatusPoisonEffect()));
        AddFace (new DiceFace(new StatusPoisonEffect()));
        currentFace = faceList[0];
    }

    public void CreateFightDice() {
        diceType = 4;
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new FightArmorMode()));
        AddFace (new DiceFace(new FightArmorMode()));
        AddFace (new DiceFace(new FightDraculaMode()));
        AddFace (new DiceFace(new FightDraculaMode()));
        AddFace (new DiceFace(new FightTwoBulletsMode()));
        AddFace (new DiceFace(new FightTwoBulletsMode()));
        currentFace = faceList[0];
    }

    public DiceFace SelectRandomFace() {
        int index = Random.Range(0, faceList.Count); // TODO Change to add weight
        currentFace = faceList[index];
        currentFace.effect.ActivateEffect();

        return currentFace;
    }

    public DiceFace ChangeCurrentFace(int newFaceId)
    {
        currentFace = faceList[newFaceId-1];
        currentFace.effect.ActivateEffect();
        DiceRack.Instance.NewCurrentFace(currentFace);

        return currentFace;
    }

    public DiceFace GetCurrentFace()
    {
        return currentFace;
    }

    public List<DiceFace> getFaceList()
    {
        return faceList;
    }

    public string getTitle()
    {
        return currentFace.effect.getTitle();
    }
    public string getHtmlText()
    {
        return currentFace.effect.getHtmlText();
    }

    public override string ToString()
    {
        return currentFace.effect.ToString();
    }
}
