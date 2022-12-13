using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthBuff")]
public class HealthBuff : PowerupEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        if (target.GetComponent<Health>().health + amount > 100)
        {
            target.GetComponent<Health>().health = 100;
            target.GetComponent<Health>().slider.value = 100;
        }
        else
        {
            target.GetComponent<Health>().health += amount;
            target.GetComponent<Health>().slider.value = target.GetComponent<Health>().health + amount;
        }
    }
}
