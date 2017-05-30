using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour
{
    Transform player;
    Transform playerCam;
    public static float throwForce = 20;
    private bool hasPlayer = false; // the item is within the player's reach
    public bool beingCarried = false; // currently in the process of being picked up or carried around
    private bool canThrow = false; // the player is no longer pressing the Action button, the pickup process has stopped or is finished
    private bool touched = false; // the item touched a wall
    private bool highlight = false; // highlight the item when it's possible to pick it up
    private Material materialNormal;
    public Material materialHL;
    ManipulateTime manipulateTime;
    Renderer mainRenderer;
    Renderer subRenderer;

    private void Start()
    {
        // fetch material from parent or first child
        materialNormal = FetchMaterial();
        manipulateTime = gameObject.GetComponent<ManipulateTime>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0].transform;
        playerCam = player.GetChild(0);
    }

    Material FetchMaterial()
    {
        // fetch material from parent or first child
        mainRenderer = gameObject.GetComponent<Renderer>();
        subRenderer = gameObject.GetComponentInChildren<Renderer>();

        if (mainRenderer)
        {
            return mainRenderer.material;
        }
        else if (subRenderer)
        {
            return subRenderer.material;
        }
        else
        {
            return null;
        }
    }

    void Update()
    {
        Material currentMaterial = FetchMaterial();
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f && gameObject.GetComponent<Renderer>().isVisible)
        {
            hasPlayer = true;
        }
        else if (hasPlayer)
        {
            hasPlayer = false;
        }

        if (hasPlayer && Input.GetButtonDown("Action") && beingCarried == false)
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

            gameObject.transform.position = gameObject.transform.position + Vector3.up * 0.3f;
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
                manipulateTime.updateTime();
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
                manipulateTime.updateTime();
            }
            // else if (Input.GetMouseButtonDown(1)) //right click, 0 is left click
            else if (canThrow && beingCarried && Input.GetButtonDown("Throw"))
            {
                canThrow = false;
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce * 10);
                manipulateTime.updateTime();
            }
        }

        highlight = hasPlayer && !canThrow;

        if (highlight && currentMaterial != materialHL)
        {
            if (mainRenderer)
            {
                gameObject.GetComponent<Renderer>().material = materialHL;
            }
            else if (subRenderer)
            {
                gameObject.GetComponentInChildren<Renderer>().material = materialHL;
            }
        }
        else if (!highlight && currentMaterial != materialNormal)
        {
            if (mainRenderer)
            {
                gameObject.GetComponent<Renderer>().material = materialNormal;
            }
            else if (subRenderer)
            {
                gameObject.GetComponentInChildren<Renderer>().material = materialNormal;
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
