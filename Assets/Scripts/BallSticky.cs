<<<<<<< Updated upstream
﻿using System.Collections;
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

  
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSticky : MonoBehaviour
{
    Ball ballScript;
    Rigidbody StickyballPhysics;
    Vector3 collisionPosition;
    [SerializeField] bool ballShouldBeStuck = false; //serialized for debugging
    [SerializeField] int timeToWait = 3;
    Coroutine dSticking;//storing referance to the coroutine DisableStickingAfterTime to be abe to stop it
    [SerializeField] bool ballIsSticky = true;//for debuging 
    [SerializeField] Material ballMaterial;
    [SerializeField] Material stickyMaterial;
    [SerializeField] bool enableColorChangeOnTypeSwitch =true;
    private void OnCollisionEnter(Collision collision)
    {
        if (ballIsSticky == false)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            return;
        }
        else
        {                                       //TODO ADD A IF not NUll do , else return; (Zoldyak)
            StickyballPhysics.useGravity = false;
            StickyballPhysics.angularVelocity = Vector3.zero;
            StickyballPhysics.velocity = Vector3.zero;
            ballShouldBeStuck = true;
            if (collision.gameObject.tag == "Ground")
            {
                return;
            }
            else
            {
                dSticking = StartCoroutine(DisableStikcingAfterTime());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StickyballPhysics = gameObject.GetComponent<Rigidbody>();
        ballScript = GetComponent<Ball>();
    }


    private void Update()
    {
        SwitchBallType();
        StickBallToObject();
        collisionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void StickBallToObject()
    {
        if (ballShouldBeStuck)
        {
            transform.position = collisionPosition;
        }
        else if (ballShouldBeStuck == false)
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

    public void StopDisableStikcingAfterTimeCoroutine()
    {
        if (dSticking != null)
        {
            StopCoroutine(dSticking);
        }
    }
    public void SwitchBallType()
    {

        if (ballScript.IsThisBallPickedUp() && Input.GetMouseButtonDown(1))
        {
            if (ballIsSticky == true)
            {
                ballIsSticky = false;
                if(enableColorChangeOnTypeSwitch)
                { 
                    gameObject.GetComponent<MeshRenderer>().material = ballMaterial;
                    gameObject.GetComponentInChildren<Light>().color = Color.white;
                }
                else
                {
                    return;
                }
            }
            else if (ballIsSticky == false)
            {
                ballIsSticky = true;
                if(enableColorChangeOnTypeSwitch)
                { 
                    gameObject.GetComponent<MeshRenderer>().material = stickyMaterial;
                    gameObject.GetComponentInChildren<Light>().color = Color.red;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

    }
}

>>>>>>> Stashed changes
