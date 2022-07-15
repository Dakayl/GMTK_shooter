using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct DiceFace {
    public IDiceEffect effect;
    
    public DiceFace(IDiceEffect effect) {
        this.effect = effect;
    }
}

public class Dice : MonoBehaviour
{
    private List<DiceFace> faceList;
    private DiceFace currentFace;
    public DiceFace face { get { return currentFace }}
    private int currentFaceIndex;

    public Awake() {
       
    }

    public AddFace(DiceFace face){
        faceList.Add(face)
        totalWeight += face.weight;
    }

    public void CreateInteractionDice() {
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new InteractionBounceEffect);
        AddFace (new DiceFace(new InteractionBounceEffect);
        AddFace (new DiceFace(new InteractionBounceEffect);
        AddFace (new DiceFace(new InteractionPiercingEffect);
        AddFace (new DiceFace(new InteractionPiercingEffect);
        AddFace (new DiceFace(new InteractionPiercingEffect);
    }

    public void CreateShootDice() {
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new ShootDamageEffect);
        AddFace (new DiceFace(new ShootDamageEffect);
        AddFace (new DiceFace(new ShootRangeEffect);
        AddFace (new DiceFace(new ShootRangeEffect);
        AddFace (new DiceFace(new ShootSpeedEffect);
        AddFace (new DiceFace(new ShootSpeedEffect);
    }

     public void CreateStatusDice() {
        faceList = new List<DiceFace>();
        AddFace (new DiceFace(new StatusFireEffect);
        AddFace (new DiceFace(new StatusFireEffect);
        AddFace (new DiceFace(new StatusFireEffect);
        AddFace (new DiceFace(new StatusPoisonffect);
        AddFace (new DiceFace(new StatusPoisonffect);
        AddFace (new DiceFace(new StatusPoisonffect);
    }

    public DiceFace SelectRandomFace() {
        int index = Random.Range(0, faceList.Count); // TODO Change to add weight
        currentFaceIndex = index;
        currentFace = faceList[index];
        currentFace.effect.ActivateEffect();
        return currentFace
    }
}
