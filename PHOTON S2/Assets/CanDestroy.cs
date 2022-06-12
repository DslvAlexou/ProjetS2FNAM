using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using System.IO;
public class CanDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float degreesPerSecond = 50;
    private void Update()
    {
        this.transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerMP")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "CAT")
        {
            Destroy(this.gameObject);
        }
    }
}
