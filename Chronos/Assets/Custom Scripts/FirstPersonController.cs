using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonController : MonoBehaviour {
	// stair grounded
	private RaycastHit stairRaycast;
	bool firstStairCast = false;
	private RaycastHit stairRaycast2;
	bool secondStairCast = false;
	
	// head bob movement
	float minimum = 0.624F;
	float maximum = 0.67F;
	float duration = 3.2F;
	bool upwards = true;

	// references
	//public GameManager manager;
	
	// public vars
	public float mouseSensitivityX = 250;
	public float mouseSensitivityY = 250;
	public float jumpCD = 0.0F;
	public float walkSpeed = 6; //movement/walking speed
	public float jumpForce = 260; //jump height/strength
	public float jumpDamping = 3.5f; // reduced movement while jumping
	public LayerMask groundedMask; //mask for raytracing/jumping - reference plane for the raycast#

	// General Audio
	private AudioSource walk;
    private AudioSource jump;

	// System vars
	public bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;

	// find out if player is inactive for 30 seconds or more (to get back to the HUB)
	private float inactiveSeconds = 0.0f;
	private float maxInactiveSeconds = 50.0f;
	private float lastMouseX = 0.0f;
	private float lastMouseY = 0.0f;
	
	void Start() { //Awake
		Cursor.visible = false;
		Screen.lockCursor = true;
		cameraTransform = Camera.main.transform;
        var audioSources = GetComponents<AudioSource>();
        this.walk = audioSources[0];
        this.jump = audioSources[1];
	}

	void Update() {
		// check if player was inactive
		float thisMouseX = Input.GetAxis ("Mouse X");
		float thisMouseY = Input.GetAxis ("Mouse Y");

		if (thisMouseX == this.lastMouseX &&
			thisMouseY == this.lastMouseY) {

			this.inactiveSeconds += Time.deltaTime;
		} else {
			this.inactiveSeconds = 0.0f;
		}

		// if too long inactive, go back to HUB
		if (this.inactiveSeconds > this.maxInactiveSeconds) {
			//AutoFade.LoadLevel("Central" , 1, 1, Color.black);
		}

		// play the walking sound if player walked enough
		if (IsGrounded() && ((Input.GetAxisRaw("Vertical")!= 0) || Input.GetAxisRaw("Horizontal")!= 0)) {
			if (walk.isPlaying == false) {
				this.walk.Play ();
			}
		} else {
			this.walk.Stop ();
		}
        
		// set dampig dependend if grounded or not
		float damping;
		if (IsGrounded()) {
			damping = 1;
		} else {
			damping = jumpDamping;
		}
		// Look rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,80);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");
		
		Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f * damping); //ref allows to modify a global variable
		
		// Jump
		if (Input.GetButtonDown("Jump") && jumpCD <= 0) {
			if (IsGrounded()) {
				GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);

                // also play audio sound
                this.jump.Play();

                jumpCD = 0.3F;
			}
		}

		if (IsGrounded () && ((Input.GetAxisRaw("Vertical")!= 0) || Input.GetAxisRaw("Horizontal")!= 0)) {
			if (upwards) {
				cameraTransform.localPosition += new Vector3 (0, Time.deltaTime/duration, 0);
				float yPos = cameraTransform.localPosition.y;
				if(yPos >= maximum){
					upwards = false;
				}
			}else{
				cameraTransform.localPosition -= new Vector3 (0, Time.deltaTime/duration, 0);
				float yPos = cameraTransform.localPosition.y;
				if(yPos <= minimum){
					upwards = true;
				}
			}
		}

		if (jumpCD > 0) {
			jumpCD -= Time.deltaTime;
		}
	}
	
	bool IsGrounded ()
	{
        //Debug.DrawRay(transform.position, -transform.up, Color.red, 0.1f);
		bool ground = Physics.Raycast (transform.position, - transform.up, 1 + 0.3f, groundedMask);
        grounded = (ground || IsStairGrounded());
        return grounded; //letzter Parameter groundedMask
	}

	bool IsStairGrounded(){ 
		Vector3 second  = transform.position;
		second.x += 0.05F;
		if (Physics.Raycast (transform.position, -transform.up, out stairRaycast, 2.5F) &&
		        (stairRaycast.collider.gameObject.tag == "Stair")) {
				firstStairCast = true;
		} else {
			firstStairCast = false;
		}

		if (Physics.Raycast (second, -transform.up, out stairRaycast2, 2.5F) &&
		    (stairRaycast2.collider.gameObject.tag == "Stair")) {
			secondStairCast = true;
		}else{
			secondStairCast = false;
		}

		if(secondStairCast && firstStairCast){
			return true;
		}
		return false;
	}

	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime; //transform to local space (instead of world space - move on the surface of the sphere)
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + localMove);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Goal")
		{
			//manager.CompleteLevel();
		}
	}

    public void ChangeMouseSensitivity(float sensitivity)
    {
        mouseSensitivityX = sensitivity;
        mouseSensitivityY = sensitivity;
        string sensitivityString = sensitivity.ToString();
    }
}