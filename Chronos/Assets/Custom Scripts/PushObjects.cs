using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour {

    //The list of colliders currently inside the trigger
    List<Collider> triggerList;
    public float standardThrust = 10;
    private float thrust = 0;

    // Use this for initialization
    void Start () {
        triggerList = new List<Collider> ();
        thrust = standardThrust;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PushColliders()
    {
        //activate enemies
        foreach (Collider collider in triggerList)
        {
            Rigidbody rigidBody = collider.gameObject.GetComponent<Rigidbody>();
            if (rigidBody)
            {
                rigidBody.AddForce(transform.up * thrust * 100);
            }
        }
    }

    public void increaseThrust()
    {
        thrust = standardThrust * 2;
    }

    public void resetThrust()
    {
        thrust = standardThrust;
    }
 
    //called when something enters the trigger
    void OnTriggerEnter(Collider other)
    {
        //if the object is not already in the list
        if (!triggerList.Contains(other))
        {
            //add the object to the list
            triggerList.Add(other);
        }
    }

    //called when something exits the trigger
    void OnTriggerExit(Collider other)
    {
        //if the object is in the list
        if (triggerList.Contains(other))
        {
            //remove it from the list
            triggerList.Remove(other);
        }
    }
}
