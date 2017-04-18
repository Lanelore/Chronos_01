using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float throwForce = 10;
    private bool hasPlayer = false; // the item is within the player's reach
    private bool beingCarried = false; // currently in the process of being picked up or carried around
    private bool canThrow = false; // the player is no longer pressing the Action button, the pickup process has stopped or is finished
    private bool touched = false; // the item touched a wall
    private bool highlight = false; // highlight the item when it's possible to pick it up
    private Material materialNormal;
    public Material materialHL;

    private void Start()
    {
        materialNormal = gameObject.GetComponent<Renderer>().material;
    }

    void Update()
    {
        Material currentMaterial = gameObject.GetComponent<Renderer>().material;
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f && gameObject.GetComponent<Renderer>().isVisible)
        {
            hasPlayer = true;
        }
        else if (hasPlayer)
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

        highlight = hasPlayer && !canThrow;

        if (highlight && currentMaterial != materialHL)
        {
            gameObject.GetComponent<Renderer>().material = materialHL;
        }
        else if (!highlight && currentMaterial != materialNormal)
        {
            gameObject.GetComponent<Renderer>().material = materialNormal;
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
