using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using System.IO;
public class Lolaspawn : MonoBehaviourPunCallbacks
{
    public bool alreadyLola = false;
    void Start()
    {
        if (!alreadyLola)
        {
            Quaternion suu = Quaternion.Euler(-90, 0, 0);
            Vector3 pos = new Vector3(5, 0, 5);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Lola"), pos, suu);
            alreadyLola = true;
        }
    }
}
