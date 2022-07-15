using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct DiceFace {
    public int weight;
    public IDiceEffect effect;
    
    public DiceFace(IDiceEffect effect, int weight = 1) {
        this.weight = weight;
        this.effect = effect;
    }
}

public class Dice : MonoBehaviour
{
    private List<DiceFace> faceList;
    private int currentFaceIndex;
    private DiceFace currentFace;
    public Awake() {
        faceList = new List<DiceFace>();
    }

    public AddFace(DiceFace face){
        faceList.Add(face)
    }

    public DiceFace SelectRandomFace() {
        int index = Random.Range(0, faceList.Count);
        currentFaceIndex = index;
        currentFace = faceList[index];
        currentFace.effect.ActivateEffect();
        return currentFace
    }
}
