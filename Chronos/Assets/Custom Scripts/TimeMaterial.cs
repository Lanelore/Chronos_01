using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMaterial : MonoBehaviour {

    public float time = 1;
    ManipulateTime mt;
    public Material playMaterial;
    public Material freezeMaterial;
    public Material ffMaterial;

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
            UpdateMaterial();
        }
    }

    void UpdateMaterial()
    {
        Renderer timeRenderer = gameObject.GetComponent<Renderer>();

        if (time == 0)
        {
            timeRenderer.material = freezeMaterial;
        }
        else if (time == 1)
        {
            timeRenderer.material = playMaterial;
        }
        else if (time == 2)
        {
            timeRenderer.material = ffMaterial;
        }
    }
}
