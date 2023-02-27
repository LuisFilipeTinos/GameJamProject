using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public Slider staminaBar;

    private int maxStamina = 100;
    private int currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    public static StaminaSystem instance;
    public static bool haveStamina;

    private void Awake()
    {
        instance= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;

        haveStamina = true;
    }

    // Update is called once per frame
    public void UseStamina(int amount)
    {
        
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            StartCoroutine(RegenStamina());
            haveStamina = true;
        }
        else
        {
           haveStamina = false;
        }
              
        
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;

        }
    }

}
