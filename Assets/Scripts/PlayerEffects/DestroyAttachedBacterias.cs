using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAttachedBacterias : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        foreach(Transform children in other.gameObject.transform)
        {
            if(children.gameObject.GetComponent<Bacteria>() != null)
            {
                Destroy(children.gameObject);
            }
        }
    }
}
