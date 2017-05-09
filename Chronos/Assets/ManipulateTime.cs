﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateTime : MonoBehaviour {

    public float time = 1;
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;
    private Rigidbody rb;
    private Animator an;
    public bool toPause = false;
    public bool toResume = false;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        an = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Freeze") && time != 1)
        {
            time = 1;
            toResume = true;
        }
        else if (Input.GetButtonDown("Freeze") && time != 0)
        {
            time = 0;
            toPause = true;
        }
    }

    void FixedUpdate()
    {
        ThrowObject throwObject = gameObject.GetComponent<ThrowObject>();
        if (throwObject && throwObject.beingCarried)
            return;        

        if (toPause && time == 0)
        {
            PauseGame();
        }
        else if (toResume && time != 0)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        toPause = false;

        if (an != null)
        {
            an.enabled = false;
        }
        else
        {
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true;
        }
    }

    public void ResumeGame()
    {
        toResume = false;

        if (an != null)
        {
            an.enabled = true;
        }
        else
        {
            rb.isKinematic = false;
            rb.AddForce(savedVelocity, ForceMode.VelocityChange);
            rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
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
            PauseGame();
        }
    }
}
