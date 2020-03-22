using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSticky : MonoBehaviour
{
    Rigidbody StickyballPhysics;
    Vector3 collisionPosition;
    [SerializeField]bool ballShouldBeStuck = false; //serialized for debugging
    [SerializeField] int timeToWait = 3;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            return;
        }
        else 
        { 
         StickyballPhysics.useGravity = false;
         StickyballPhysics.angularVelocity = Vector3.zero;
         StickyballPhysics.velocity = Vector3.zero;
         ballShouldBeStuck = true;
            if(collision.gameObject.tag == "Ground")
            {
               return;
            }
            else
            {
               StartCoroutine(DisableStikcingAfterTime());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        StickyballPhysics = gameObject.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        StickBallToObject();
        collisionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void StickBallToObject()
    {
        if(ballShouldBeStuck)
        { 
            transform.position = collisionPosition;
        }
        else if(ballShouldBeStuck == false)
        {
            StickyballPhysics.useGravity = true;

        }
        else
        {
            return;
        }

    }

    public void DisableSticking()
    {
        ballShouldBeStuck = false;
    }
    IEnumerator DisableStikcingAfterTime()
    {
        yield return new WaitForSeconds(timeToWait);
        DisableSticking();
    }
}

  