using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using System.IO;

public class SoupSpawn : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float period = 1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextActionTime)
        { 	
            nextActionTime = Time.time + period;
            int hasard = Random.Range(0, 3);
            if (hasard == 0)
            {
                Quaternion suu = Quaternion.Euler(15, 0, 0);
                Vector3 pos = new Vector3(Random.Range(0, 200), 1, Random.Range(-200,10));
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Can_1"),pos,suu);
            }
            if (hasard == 1)
            {
                Quaternion suu = Quaternion.Euler(15, 0, 0);
                Vector3 pos = new Vector3(Random.Range(0, 200), 1, Random.Range(-200,10));
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Can_2"),pos,suu);
            }
            if (hasard == 2)
            {
                Quaternion suu = Quaternion.Euler(15, 0, 0);
                Vector3 pos = new Vector3(Random.Range(0, 200), 0.5f, Random.Range(-200,10));
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Can_3"),pos,suu);
            }
        }
    }
    
}
