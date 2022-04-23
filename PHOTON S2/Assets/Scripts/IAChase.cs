using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using System.IO;

public class IAChase : MonoBehaviourPunCallbacks
{
    //public NavMeshAgent EnemyNav;
    GameObject Player = null;
    bool FoundPlayer = false;
    public PhotonView PlayerView;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;
    Vector3 playerPosition = Vector3.zero;

///////////////////////////////////////////////////////////
    public LayerMask PlayerLayer;
    public float OnPlayerSenseRadius;
    Collider[] TestOverlap = null;
    public int MaxDist;
    AudioSource m_MyAudioSource;
    public float speed = 1.0f;
///////////////////////////////////////////////////////////



    private void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        TestOverlap = Physics.OverlapSphere(transform.position, OnPlayerSenseRadius, PlayerLayer);
        
        if(Vector3.Distance(this.transform.position,playerPosition) >= MaxDist)
        {
            m_MyAudioSource.Play();
        } 

        if (FoundPlayer)
        {
            this.transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
            playerPosition = Player.transform.position;
            // Determine which direction to rotate towards
            Vector3 targetDirection = playerPosition - this.transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        if (!FoundPlayer)
        {
            for (int i = 0; i < TestOverlap.Length; i++)
            {
                if (TestOverlap.GetValue(i) != null)
                {
                    if (GameObject.FindGameObjectWithTag("PlayerMP").GetInstanceID() == TestOverlap[0].gameObject.GetInstanceID())
                    {
                        Player = GameObject.FindGameObjectWithTag("PlayerMP").gameObject;
                        playerPosition = Player.transform.position;
                        FoundPlayer = true;
                    }
                }

            }
        }

    }
}