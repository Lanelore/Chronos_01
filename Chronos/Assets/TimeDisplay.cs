using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour {

    public float time = 1;
    public bool toPause = false;
    public bool toResume = false;

    // Use this for initialization
    void Start () {

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
        Text timeText = gameObject.GetComponent<Text>();
        timeText.color = Color.red;
        timeText.text = "Time x0";
    }

    public void ResumeGame()
    {
        toResume = false;
        Text timeText = gameObject.GetComponent<Text>();
        timeText.color = Color.green;
        timeText.text = "Time x1";
    }

    public void updateTime()
    {
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
