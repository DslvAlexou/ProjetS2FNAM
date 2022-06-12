using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : Timer
{

    public Slider staminaBar;
    public float currentStamina;
    public float MaxStamina = 500f;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;
    public static StaminaBar instance;
    public static bool canrun;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentStamina = MaxStamina;
        staminaBar.maxValue = MaxStamina;
        staminaBar.value = MaxStamina;
        canrun = true;
    }

    public void UseStamina(int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("Not Enough Stamina");
            canrun = false;
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < MaxStamina)
        {
            currentStamina += MaxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            UseStamina(1);
        if (currentStamina >= 10)
            canrun = true;
    }
}