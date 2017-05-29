using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateTime : MonoBehaviour {

    public float time = 1;
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;
    private Rigidbody rb;
    private Animator an;
    private PushObjects po;
    public bool toFreeze = false;
    public bool toFastForward = false;
    public bool toResume = false;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        an = gameObject.GetComponent<Animator>();
        po = gameObject.GetComponent<PushObjects>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Freeze") && time != 0)
        {
            time = 0;
            toFreeze = true;
        }
        else if (Input.GetButtonDown("Fast Forward") && time != 2)
        {
            time = 2;
            toFastForward = true;
        }
        else if ((Input.GetButtonDown("Freeze") || Input.GetButtonDown("Fast Forward")) && time != 1)
        {
            time = 1;
            toResume = true;
        }
    }

    void FixedUpdate()
    {
        ThrowObject throwObject = gameObject.GetComponent<ThrowObject>();
        if (throwObject && throwObject.beingCarried)
            return;    

        if (toFreeze && time == 0)
        {
            FreezeGame();
        }
        else if (toFastForward && time != 0)
        {
            FastForwardGame();
        }
        else if (toResume && time != 0)
        {
            ResumeGame();
        }
    }

    public void FreezeGame()
    {
        toFreeze = false;

        if (an != null)
        {
            an.speed = 0;
        }
        else if (rb != null)
        {
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true;
        }

        if (po != null)
        {
            po.resetThrust();
        }
    }

    public void FastForwardGame()
    {
        toFastForward = false;

        if (an != null)
        {
            an.speed = 2;
        }
        // resume rigidbodies if they are still frozen
        else if (rb != null && rb.isKinematic)
        {
            rb.isKinematic = false;
            rb.AddForce(savedVelocity, ForceMode.VelocityChange);
            rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
        }

        if (po != null)
        {
            po.increaseThrust();
        }
    }


    public void ResumeGame()
    {
        toResume = false;

        if (an != null)
        {
            an.speed = 1;
        }
        else if (rb != null && rb.isKinematic)
        {
            rb.isKinematic = false;
            rb.AddForce(savedVelocity, ForceMode.VelocityChange);
            rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
        }

        if (po != null)
        {
            po.resetThrust();
        }
    }

    public void updateTime()
    {
        savedVelocity = Vector3.zero;
        savedAngularVelocity = Vector3.zero;

        if (time == 1)
        {
            ResumeGame();
        }
        else if (time == 0)
        {
            FreezeGame();
        }
    }
}
