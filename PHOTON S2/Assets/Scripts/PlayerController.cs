using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : StaminaBar
{


	[SerializeField] GameObject cameraHolder;

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
	Collider[] TestOverlap = null;
	public int MaxDist;
	Vector3 playerPosition = Vector3.zero;
	public Slider HealthBar;
	GameObject Player = null;
	bool FoundPlayer = false;

    void Awake()
	{
		rb = GetComponent<Rigidbody>();
		PV = GetComponent<PhotonView>();
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
		TestOverlap = Physics.OverlapSphere(transform.position, OnPlayerSenseRadius, PlayerLayer);
		if (!FoundPlayer)
		{
			for (int i = 0; i < TestOverlap.Length; i++)
			{
				if (TestOverlap.GetValue(i) != null)
				{
					if (GameObject.FindGameObjectWithTag("CAT").GetInstanceID() == TestOverlap[0].gameObject.GetInstanceID())
					{
						Player = GameObject.FindGameObjectWithTag("CAT").gameObject;
						playerPosition = Player.transform.position;
						FoundPlayer = true;
					}
				}

			}
		}
        
		if(Vector3.Distance(this.transform.position,playerPosition) <= MaxDist)
		{
			Health -= 1;
		}

		HealthBar.value = Health;
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
    }

}