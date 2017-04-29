using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateTime : MonoBehaviour {

    private float time = 1;
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;
    private Rigidbody rb;
    private Animator an;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        an = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Freeze") && time != 0)
        {
            time = 0;
            PauseGame();
        }
        else if (Input.GetButtonDown("Freeze") && time == 0)
        {
            time = 1;
            ResumeGame();
        }

    }

    void PauseGame()
    {
        savedVelocity = rb.velocity;
        savedAngularVelocity = rb.angularVelocity;
        rb.isKinematic = true;

        if (an != null)
        {
            an.enabled = false;
        }
    }

    void ResumeGame()
    {
        rb.isKinematic = false;
        rb.AddForce(savedVelocity, ForceMode.VelocityChange);
        rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);

        if (an != null)
        {
            an.enabled = true;
        }
    }
}
