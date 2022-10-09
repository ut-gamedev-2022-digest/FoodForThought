using System;
using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float _speed = 5f;

    private void Awake()
    {
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var speedMultiplier = (Time.deltaTime * _speed);
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speedMultiplier);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speedMultiplier);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speedMultiplier);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speedMultiplier);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speedMultiplier);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * speedMultiplier);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * (speedMultiplier * 20));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.down * (speedMultiplier * 20));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
    }
}
