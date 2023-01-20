using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour
{
    public string Tag = "Player";
    public bool Activated = false;

    private void Awake()
    {
        Activated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag))
        {
            Activated = !Activated;
        }
    }
}
