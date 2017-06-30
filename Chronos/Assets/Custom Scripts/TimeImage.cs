using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeImage : MonoBehaviour {

    public float time = 1;
    ManipulateTime mt;
    public Sprite playSprite;
    public Sprite freezeSprite;
    public Sprite ffSprite;

    // Use this for initialization
    void Start () {
        mt = gameObject.GetComponent<ManipulateTime>();
        if (mt == null)
        {
            mt = gameObject.transform.parent.gameObject.GetComponent<ManipulateTime>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (time != mt.time)
        {
            time = mt.time;
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        Image timeImage = gameObject.GetComponent<Image>();

        if (time == 0)
        {
            timeImage.sprite = freezeSprite;
        }
        else if (time == 1)
        {
            timeImage.sprite = playSprite;
        }
        else if (time == 2)
        {
            timeImage.sprite = ffSprite;
        }
    }
}
