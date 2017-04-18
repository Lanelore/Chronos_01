using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float throwForce = 10;
    public bool hasPlayer = false; // the item is within the player's reach
    public bool beingCarried = false; // currently in the process of being picked up or carried around
    public bool canThrow = false; // the player is no longer pressing the Action button, the pickup process has stopped or is finished
    private bool touched = false; // the item touched a wall

    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f && gameObject.GetComponent<Renderer>().isVisible)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetButtonDown("Action"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            /*
            float objectSize = gameObject.GetComponent<Renderer>().bounds.size.magnitude;
            Vector3 hoverPosition = playerCam.transform.position + playerCam.transform.forward * objectSize;
            float newDist = Vector3.Distance(hoverPosition, player.position);
            if (newDist < dist)
            {
                return;
                gameObject.transform.position = hoverPosition;
            }
            */
            gameObject.transform.position = gameObject.transform.position + Vector3.up * 0.1f;
            transform.parent = playerCam;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                canThrow = false;
                touched = false;
            }
            if (Input.GetButtonUp("Action") && beingCarried)
            {
                canThrow = true;
            }
            if (canThrow && beingCarried && Input.GetButtonDown("Action"))
            {
                canThrow = false;
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
            }
            else if (Input.GetMouseButtonDown(1)) //right click, 0 is left click
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (beingCarried && !other.CompareTag("Player"))
        {
            touched = true;
        }
    }
}
