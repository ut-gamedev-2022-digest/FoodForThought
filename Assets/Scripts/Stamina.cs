using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float stamina = 100f;
    public StaminaBar staminaBar;
    public float maxStamina = 100f;
    public Slider slider;
    public int sprintSpeed;
    private ThirdPersonController trdPersonController;


    private void Awake()
    {
        trdPersonController = GetComponent<ThirdPersonController>();
    }
    private void Start()
    {
        staminaBar.SetStamina(maxStamina);
    }

    private void Update()
    {
        trdPersonController.SprintSpeed = trdPersonController.MoveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && stamina>=0f)
        {
            trdPersonController.SprintSpeed = sprintSpeed;
            stamina = slider.value;
            stamina -= (10f * Time.deltaTime);
            staminaBar.SetStamina(stamina);
        }
        if ((stamina<=100f) && (!Input.GetKey(KeyCode.LeftShift)))
        {
            stamina = slider.value;
            stamina += Time.deltaTime * 50f / 40f;
            staminaBar.SetStamina(stamina);
        }
    }
}
