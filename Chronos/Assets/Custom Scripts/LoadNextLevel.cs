using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Escape"))
        {
            Application.Quit();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        print("Trigger Door");
        if (other.gameObject.tag == "Player")
        {
            print("Trigger Player");
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            print("next " + nextScene + "; count " + SceneManager.sceneCount);
            if (nextScene <= SceneManager.sceneCount)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
