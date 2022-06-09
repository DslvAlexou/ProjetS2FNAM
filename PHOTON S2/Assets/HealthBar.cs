using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private int MaxHealth = 3;
    public Slider HealthBa;
    int currentHealth;
    void Start()
    {
        currentHealth = MaxHealth;
        HealthBa.maxValue = MaxHealth;
        HealthBa.value = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        currentHealth -= 1;
        HealthBa.value = currentHealth;
        Debug.Log("Hello: ");
    }
}
