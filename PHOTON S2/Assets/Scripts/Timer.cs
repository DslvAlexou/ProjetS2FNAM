using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float currentTime;
    public Text currentTimeText;


    void Start()
    {
        currentTime = 0f;
    }
    void Update()
    {
        currentTime += 1 * Time.deltaTime ;
        currentTimeText.text = currentTime.ToString();
    }
}
