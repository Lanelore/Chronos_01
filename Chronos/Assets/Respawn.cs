using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerExit(Collider other)
    {
        
        SpawnPosition spawnPos = other.gameObject.GetComponent<SpawnPosition>();
        if (spawnPos)
        {
            other.gameObject.transform.position = spawnPos.spawnPosition;
            other.gameObject.transform.rotation = Quaternion.Euler(spawnPos.spawnRotation);
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
