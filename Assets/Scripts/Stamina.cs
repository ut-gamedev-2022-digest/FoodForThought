using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float stamina = 100f;
    public StaminaBar staminaBar;
    public float maxStamina = 100f;
    public Slider slider;

    private void Start()
    {
        staminaBar.SetStamina(maxStamina);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            stamina = slider.value - 0.1f;
            staminaBar.SetStamina(stamina);
        }
        if ((stamina<100f) && (!Input.GetKey(KeyCode.LeftShift)))
        {
            stamina = slider.value + 0.02f;
            staminaBar.SetStamina(stamina);
        }
    }
}
