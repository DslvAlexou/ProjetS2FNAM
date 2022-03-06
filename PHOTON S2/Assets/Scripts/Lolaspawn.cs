using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using System.IO;
public class Lolaspawn : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private bool spawn = false;
    void Start()
    {
        if (spawn == false)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Lola"), Vector3.zero, Quaternion.identity,0);
            spawn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn == false)
        {
            PhotonNetwork.Instantiate("Lola", Vector3.zero, Quaternion.identity);
            spawn = true;
        }
    }


}
