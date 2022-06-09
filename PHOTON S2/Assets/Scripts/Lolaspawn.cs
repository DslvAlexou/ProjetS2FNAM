using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using System.IO;
public class Lolaspawn : MonoBehaviour
{
	public LayerMask PlayerLayer;
    void Start()
    {
		Quaternion suu = Quaternion.Euler(-90, 0, 0);
        Vector3 pos = new Vector3(5, 0, 5);
		var hitColliders = Physics.OverlapSphere(pos, 10, PlayerLayer);
 		if(hitColliders.Length > 0)
		{
			return;	
		}
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "orange cat"), pos, suu);
    }
}
