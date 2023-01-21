using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float Speed = 1f;
    public float RotationSpeed = 3.0f;

    private Quaternion desiredQuat;
    private float timer = 0.0f;
    private Vector3 desiredPos;
    private float _x;
    private float _y;
    private float _z;

    private void SetNewRandomDesiredPos()
    {
        desiredPos = new Vector3(Random.Range(_x - 5.0f, _x + 5.0f), Random.Range(_y - 5.0f, _y + 5.0f), Random.Range(_z - 1.5f, _z + 1.5f));
    }

    private void SetNewRandomRotation()
    {
        desiredQuat = Quaternion.Euler(new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f));
    }
    private void Start()
    {
        Vector3 startPos = transform.position;
        _x = startPos.x;
        _y = startPos.y;
        _z = startPos.z;
        SetNewRandomDesiredPos();
        SetNewRandomRotation();
    }
    // Update is called once per frame
    void Update()
    {
        if (timer > 2)
        {
            SetNewRandomRotation();
            timer = 0.0f;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredQuat, Time.deltaTime * RotationSpeed);
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * Speed);
        if (Vector3.Distance(transform.position, desiredPos) <= 0.02f)
        {
            SetNewRandomDesiredPos();
        }
    }
}
