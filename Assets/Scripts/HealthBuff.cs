using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthBuff")]
public class HealthBuff : PowerupEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Health>().health += amount;
        target.GetComponent<Health>().slider.value = target.GetComponent<Health>().health + amount;
    }
}
