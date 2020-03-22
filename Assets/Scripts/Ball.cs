using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float ballDistance = 1.5f;
    [SerializeField] Camera playerCamera;
    [SerializeField] float ballThrowingForce = 5f;
    GameState gameState;
    public bool isThisBallPickedUp = false; //we will leave this varible to identfy the state of this specifc ball
    Rigidbody ballPhysics;
    BallSticky ballSticky;

    // Start is called before the first frame update
    private void Start()
    {
        ballSticky = GetComponent<BallSticky>();
        gameState = FindObjectOfType<GameState>();
        ballPhysics = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        MaintainBallPosInfrontOfPlayer();
        ThrowBall();
    }

    public void GetPickedByPlayer()
    {
        if (gameState.IsThereSomethingPickedUp())
        {
            return;
        }
        isThisBallPickedUp = true;
        gameState.SomethingIsPickedUp();

        ballPhysics.useGravity = false;
        ballPhysics.detectCollisions = true;//<-----------------------expermintal for later use dont delete it or this comment

        if (ballSticky != null)
        {
            ballSticky.DisableSticking();
        }
    }

    public void MaintainBallPosInfrontOfPlayer()
    {
        if (isThisBallPickedUp)
        {

            ballPhysics.angularVelocity = Vector3.zero;
            ballPhysics.velocity = Vector3.zero; //TODO Remove this line after applying force to the Rigidbody of the ball to move it around while picking it up
            transform.position = playerCamera.transform.position + playerCamera.transform.forward * ballDistance;
        }
        else
        {
            return;
        }
    }

    private void ThrowBall()
    {
        if (Input.GetMouseButtonDown(0) && isThisBallPickedUp)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;

            gameObject.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * ballThrowingForce );
            isThisBallPickedUp = false;
            gameState.NothingIsPickedUp();
            if (ballSticky != null)
            {
                ballSticky.DisableSticking();
            }
        }
    }
}
