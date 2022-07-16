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
    private int currentFaceIndex;

    public void AddFace(DiceFace face){
        faceList.Add(face);
    }

    public void CreateInteractionDice() {
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new InteractionBouncingEffect()));
        AddFace (new DiceFace(new InteractionBouncingEffect()));
        AddFace (new DiceFace(new InteractionExplosionEffect()));
        AddFace (new DiceFace(new InteractionExplosionEffect()));
        AddFace (new DiceFace(new InteractionPiercingEffect()));
        AddFace (new DiceFace(new InteractionPiercingEffect()));
    }

    public void CreateShootDice() {
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new ShootSizeEffect()));
        AddFace (new DiceFace(new ShootSizeEffect()));
        AddFace (new DiceFace(new ShootRangeEffect()));
        AddFace (new DiceFace(new ShootRangeEffect()));
        AddFace (new DiceFace(new ShootSpeedEffect()));
        AddFace (new DiceFace(new ShootSpeedEffect()));
    }

    public void CreateStatusDice() {
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new StatusFireEffect()));
        AddFace (new DiceFace(new StatusFireEffect()));
        AddFace (new DiceFace(new StatusElectricityEffect()));
        AddFace (new DiceFace(new StatusElectricityEffect()));
        AddFace (new DiceFace(new StatusPoisonffect()));
        AddFace (new DiceFace(new StatusPoisonffect()));
    }

    public void CreateFightDice() {
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new FightArmorMode()));
        AddFace (new DiceFace(new FightArmorMode()));
        AddFace (new DiceFace(new FightDraculaMode()));
        AddFace (new DiceFace(new FightDraculaMode()));
        AddFace (new DiceFace(new FightTwoBulletsMode()));
        AddFace (new DiceFace(new FightTwoBulletsMode()));
    }

    public DiceFace SelectRandomFace() {
        int index = Random.Range(0, faceList.Count); // TODO Change to add weight
        currentFaceIndex = index;
        currentFace = faceList[index];
        currentFace.effect.ActivateEffect();
        return currentFace;
    }

    public override string ToString()
    {
        return ""+currentFace.effect;
    }
}
