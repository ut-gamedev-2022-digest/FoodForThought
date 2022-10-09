using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigestiveSystem : MonoBehaviour
{
    private void Update()
    {
        
        transform.localScale *= (1 + Mathf.Sin(Time.time) * 0.0001f);
        
    }
}
