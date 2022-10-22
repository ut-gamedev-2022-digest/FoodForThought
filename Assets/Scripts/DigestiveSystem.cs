using UnityEngine;

public class DigestiveSystem : MonoBehaviour
{
    public bool isAmbientMovementOn = true;

    private void Update()
    {
        if (isAmbientMovementOn) transform.localScale *= (1 + Mathf.Sin(Time.time) * 0.0001f);
    }
}