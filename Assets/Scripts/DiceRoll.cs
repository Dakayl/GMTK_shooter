using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private DiceSide[] sides;
    [SerializeField] private Sprite[] possibleSprites;
    [SerializeField] private GameObject particlePrefab;

    private Rigidbody myRb;
    [HideInInspector]
    public Vector3 diceVelocity;
    private Dice myDice;
    private DiceRack myRack;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();

        LaunchDice();
    }

    private void Update()
    {
        diceVelocity = myRb.velocity;
    }

    public void SetFaces(DiceRack theRack, Dice theDice)
    {
        myRack = theRack;
        myDice = theDice;

        switch (myDice.diceType)
        {
            case 4:
                sides[0].SetSprite(possibleSprites[0], 1);
                sides[1].SetSprite(possibleSprites[1], 2);
                sides[2].SetSprite(possibleSprites[2], 3);
                sides[3].SetSprite(possibleSprites[2], 4);
                sides[4].SetSprite(possibleSprites[1], 5);
                sides[5].SetSprite(possibleSprites[0], 6);
                break;
            case 1:
                sides[0].SetSprite(possibleSprites[3], 1);
                sides[1].SetSprite(possibleSprites[4], 2);
                sides[2].SetSprite(possibleSprites[5], 3);
                sides[3].SetSprite(possibleSprites[5], 4);
                sides[4].SetSprite(possibleSprites[4], 5);
                sides[5].SetSprite(possibleSprites[3], 6);
                break;
            case 2:
                sides[0].SetSprite(possibleSprites[6], 1);
                sides[1].SetSprite(possibleSprites[7], 2);
                sides[2].SetSprite(possibleSprites[8], 3);
                sides[3].SetSprite(possibleSprites[8], 4);
                sides[4].SetSprite(possibleSprites[7], 5);
                sides[5].SetSprite(possibleSprites[6], 6);
                break;
            case 3:
            default:
                sides[0].SetSprite(possibleSprites[9], 1);
                sides[1].SetSprite(possibleSprites[10], 2);
                sides[2].SetSprite(possibleSprites[11], 3);
                sides[3].SetSprite(possibleSprites[11], 4);
                sides[4].SetSprite(possibleSprites[10], 5);
                sides[5].SetSprite(possibleSprites[9], 6);
                break;
        }
    }

    public void FaceDownSide(int sideId)
    {
        Debug.Log("I'm a dice and I'm down");
        // Get the opposite dice side
        myDice.ChangeCurrentFace(7 - sideId);

        GetComponent<Animator>().Play("DiceDisparition");
    }

    public void destroyDaDice()
    {
        GameObject particles = Instantiate(particlePrefab, transform.position, new Quaternion());
        particles.GetComponent<ParticleSystem>().Emit(50);

        Destroy(gameObject);
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
    }

}
