using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour {

    //The list of colliders currently inside the trigger
    List<Collider> triggerList;
    public float thrust = 10;

    // Use this for initialization
    void Start () {
        triggerList = new List<Collider> ();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushColliders()
    {
        //activate enemies
        foreach (Collider collider in triggerList)
        {
            Rigidbody rigidBody = collider.gameObject.GetComponent<Rigidbody>();
            if (rigidBody)
            {
                rigidBody.AddForce(transform.up * thrust);
                print("added force to " + collider.gameObject.name);
            }
        }
        Debug.Log("PrintEvent: called at: " + Time.time);
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
