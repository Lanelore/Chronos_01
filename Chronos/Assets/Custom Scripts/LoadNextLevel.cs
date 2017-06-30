using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadNextLevel : MonoBehaviour {

    GameObject white;

	// Use this for initialization
	void Start () {
        GameObject[] foundWhite = GameObject.FindGameObjectsWithTag("White");
        white = foundWhite[0];
        white.SetActive(false);
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
        if (other.gameObject.tag == "Player")
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene <= SceneManager.sceneCount)
            {
                SceneManager.LoadScene(nextScene);
            }
            else
            {                
                white.SetActive(true);
            }
        }
    }
}
