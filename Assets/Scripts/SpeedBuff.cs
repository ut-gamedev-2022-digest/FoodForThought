using UnityEngine;

[CreateAssetMenu(menuName = "Power-Ups/SpeedBuff")]

public class SpeedBuff : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        if (target.GetComponent<Stamina>().stamina + amount > 100)
        {
            target.GetComponent<Stamina>().stamina = 100;
            target.GetComponent<Stamina>().slider.value = 100;
        }
        else
        {
            target.GetComponent<Stamina>().stamina += amount;
            target.GetComponent<Stamina>().slider.value = target.GetComponent<Stamina>().stamina + amount;
        }
    }
}
