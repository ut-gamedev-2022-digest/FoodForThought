using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    private Rigidbody rigidbody;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
       // rigidbody.AddForce(new Vector3(0, -1.0f, 0)); 
    }
 
}
