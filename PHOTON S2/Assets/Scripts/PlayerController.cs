using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : StaminaBar
{


	[SerializeField] public GameObject cameraHolder;

	[SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
	float verticalLookRotation;
	bool grounded;
	Vector3 smoothMoveVelocity;
	Vector3 moveAmount;

	Rigidbody rb;
	
	public int Health = 3;

	PhotonView PV;
	
	PlayerManager playerManager;
	
	public LayerMask PlayerLayer;
	public float OnPlayerSenseRadius;
	public int MaxDist;
	Vector3 playerPosition = Vector3.zero;
	public Slider HealthBar;
	private float temps = 0;
	
	public Text DoA;
	


	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		PV = GetComponent<PhotonView>();
		Camera cam = gameObject.GetComponent<Camera>();
	}

    private void Start()
    {
        if(!PV.IsMine)
        {
			Destroy(GetComponentInChildren<Camera>().gameObject);
			Destroy(rb);
        }
    }


    void Update()
	{
		if(!PV.IsMine)
        {
			return;
        }
		Look();
		Move();
		Jump();
	}

	void Look()
	{
		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

		verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

		cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
		HealthBar.value = Health;
	}

	void Move()
	{
		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		if (canrun && (Input.GetKey(KeyCode.LeftShift)))
			{
				moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * sprintSpeed, ref smoothMoveVelocity, smoothTime);
			}
		else 
			moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * walkSpeed, ref smoothMoveVelocity, smoothTime);
	}

	void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			rb.AddForce(transform.up * jumpForce);
		}
	}

	public void SetGroundedState(bool _grounded)
    {
		grounded = _grounded;
    }

    private void FixedUpdate()
    {
		if(!PV.IsMine)
        {
			return;
        }
		rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
		if (Health == 0 & stop ==false)
		{
			stop = true;
			temps = currentTime;
			Camera cam = cameraHolder.GetComponent<Camera>();
			Canvas Can = cameraHolder.GetComponent<Canvas>();
			cam.clearFlags = CameraClearFlags.SolidColor;
			cam.backgroundColor = Color.black;
			int LayerIgnoreRaycast = LayerMask.NameToLayer("Default");
			gameObject.layer = LayerIgnoreRaycast;
			gameObject.tag = "Player";
			cam.cullingMask = 0;
			HealthBar.gameObject.SetActive(false);
			staminaBar.gameObject.SetActive(false);
			DoA.text = "Dead";
		}
    }

    private void OnTriggerEnter(Collider other)
    {
	    if (other.gameObject.tag == "CAT")
	    {
		    Health -= 1;
		    HealthBar.value = Health;
	    }

	    if (other.gameObject.tag == "Can")
	    {
		    sprintSpeed += 1;
	    }
		if (other.gameObject.tag == "Can2")
		{
			Health += 1;
			HealthBar.value = Health;
		}
    }
}