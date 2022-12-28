using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float Speed = 1f;
    private Vector3 desiredPos;

    private float _x;
    private float _y;
    private float _z;

    private void SetNewRandomDesiredPos()
    {
        desiredPos = new Vector3(Random.Range(_x - 4.0f, _x + 4.0f), Random.Range(_y - 5.0f, _y + 5.0f), Random.Range(_z - 1.5f, _z + 1.5f));
    }
    private void Start()
    {
        Vector3 startPos = transform.position;
        _x = startPos.x;
        _y = startPos.y;
        _z = startPos.z;
        SetNewRandomDesiredPos();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * Speed);
        if (Vector3.Distance(transform.position, desiredPos) <= 0.01f)
        {
            SetNewRandomDesiredPos();
        }
    }
}
