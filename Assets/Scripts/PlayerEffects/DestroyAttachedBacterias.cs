using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAttachedBacterias : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var fixedJoints = other.GetComponents<FixedJoint>();
        if (fixedJoints != null) {
            foreach (var fixedJoint in fixedJoints) {
                Destroy(fixedJoint.connectedBody.gameObject);
                Destroy(fixedJoint);
            }
        }
    }
}
