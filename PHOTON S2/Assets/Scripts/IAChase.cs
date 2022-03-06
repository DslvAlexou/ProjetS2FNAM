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
///////////////////////////////////////////////////////////



    private void Start()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Lola"), Vector3.zero, Quaternion.identity,0);
    }

    void Update()
    {
        
        TestOverlap = Physics.OverlapSphere(transform.position, OnPlayerSenseRadius, PlayerLayer);

        if (FoundPlayer)
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
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