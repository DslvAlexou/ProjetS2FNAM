using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float currentTime;
    public Text currentTimeText;
    static public bool stop = false;


    void Start()
    {
        currentTime = 0f;
    }
    void Update()
    {
        if (stop == false)
        {
            currentTime += 1 * Time.deltaTime;
            currentTimeText.text = currentTime.ToString();
        }
    }
}
