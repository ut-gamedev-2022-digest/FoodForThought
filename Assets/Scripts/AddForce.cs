using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbody != null)
        {
            rigidbody.AddForce(new Vector3(0.0f, -1.0f, 0.0f));
        }
    }
}
