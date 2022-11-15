using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthBuff")]
public class HealthBuff : PowerupEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Health>().currentHealth += amount;
        target.GetComponent<Health>().healthBar.SetHealth(target.GetComponent<Health>().currentHealth += amount);
    }
}
