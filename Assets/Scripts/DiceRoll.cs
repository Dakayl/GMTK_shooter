using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private DiceSide[] sides;
    [SerializeField] private Sprite[] possibleSprites;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private AudioClip[] movingDice;

    private Rigidbody myRb;
    [HideInInspector]
    public Vector3 diceVelocity;
    private Dice myDice;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();

        LaunchDice();
    }

    private void Update()
    {
        diceVelocity = myRb.velocity;
    }

    public void SetFaces(Dice theDice)
    {

        myDice = theDice;
        switch (myDice.diceType)
        {
            case 4:
                sides[0].SetSprite(possibleSprites[0], 1); // Two Bullets
                sides[1].SetSprite(possibleSprites[1], 2); // Armor
                sides[2].SetSprite(possibleSprites[2], 3); // Dracula
                sides[3].SetSprite(possibleSprites[2], 4); // Dracula
                sides[4].SetSprite(possibleSprites[1], 5); // Armor
                sides[5].SetSprite(possibleSprites[0], 6); // Two Bullets
                break;
            case 1:
                sides[0].SetSprite(possibleSprites[3], 1); //Bounce
                sides[1].SetSprite(possibleSprites[4], 2); //Explode
                sides[2].SetSprite(possibleSprites[5], 3); //Pierce
                sides[3].SetSprite(possibleSprites[5], 4); //Pierce
                sides[4].SetSprite(possibleSprites[4], 5); //Explode
                sides[5].SetSprite(possibleSprites[3], 6); //Bounce
                break;
            case 2:
                sides[0].SetSprite(possibleSprites[6], 1); //Range
                sides[1].SetSprite(possibleSprites[7], 2); //Size
                sides[2].SetSprite(possibleSprites[8], 3); //Speed
                sides[3].SetSprite(possibleSprites[8], 4); //Speed
                sides[4].SetSprite(possibleSprites[7], 5); //Size
                sides[5].SetSprite(possibleSprites[6], 6); //Range
                break;
            case 3:
            default:
                sides[0].SetSprite(possibleSprites[9], 1); //Elec
                sides[1].SetSprite(possibleSprites[10], 2); //Fire
                sides[2].SetSprite(possibleSprites[11], 3); //Poison
                sides[3].SetSprite(possibleSprites[11], 4); //Poison
                sides[4].SetSprite(possibleSprites[10], 5); //Fire
                sides[5].SetSprite(possibleSprites[9], 6); //Elec
                break;
        }
    }

    public void FaceDownSide(int sideId)
    {
        // Get the opposite dice side
        int newFaceId = 7 - sideId;
        Sprite sprite = sides[newFaceId-1].GetSprite();
        DiceFace face = myDice.ChangeCurrentFace(newFaceId, sprite);
       
  
        GetComponent<Animator>().Play("DiceDisparition");
    }

    public void destroyDaDice()
    {
        GameObject particles = Instantiate(particlePrefab, transform.position, new Quaternion());
        particles.GetComponent<ParticleSystem>().Emit(50);

        Destroy(gameObject);
    }

    IEnumerator SoundDice() {
        yield return new WaitForSeconds(0.4f);
        playRandomFromArray(movingDice, 0.8f);
    }

    private void LaunchDice()
    {
        Vector3 direction;
        direction.x = Random.Range(-500, 500);
        direction.y = Random.Range(-500, 500);
        direction.z = Random.Range(-500, 500);
        transform.position = new Vector3(0, 0, -10);
        myRb.AddForce(direction.normalized * 300);
        myRb.AddTorque(direction);
        StartCoroutine(SoundDice());
    }

    public void playRandomFromArray(AudioClip[] listClips, float volume){
        if(listClips.Length < 1) return;
        int index = Random.Range(0, listClips.Length);
        if(listClips[index] != null) {
            AudioClip clip = listClips[index];
            SoundPlayer.Play(clip, volume);
        }
    }

}
