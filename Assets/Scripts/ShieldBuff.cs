using UnityEngine;

[CreateAssetMenu(menuName = "Power-Ups/ShieldBuff")]

public class ShieldBuff : PowerupEffect
{
    private float count;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Health>()._isShielded = true;
    }
}
