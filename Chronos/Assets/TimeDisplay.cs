using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour {

    public float time = 1;
    ManipulateTime mt;

    // Use this for initialization
    void Start () {
        mt = gameObject.GetComponent<ManipulateTime>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (time != mt.time)
        {
            time = mt.time;
            UpdateText();
        }
    }

    void UpdateText()
    {
        Text timeText = gameObject.GetComponent<Text>();
        timeText.text = "Time x" + time;

        if (time == 0)
        {
            timeText.color = Color.red;
        }
        else if (time == 1)
        {
            timeText.color = Color.green;
        }
        else if (time == 2)
        {
            timeText.color = Color.blue;
        }
    }
}
