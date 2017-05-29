using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour {

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    // Use this for initialization
    void Start () {
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.rotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
