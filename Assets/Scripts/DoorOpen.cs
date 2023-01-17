using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float CooldownTime = 10f;
    public float AnimationSpeed = 1f;
    public bool Automatic;
    
    
    Animator animator;

    private float _openTime;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.speed = AnimationSpeed;
        _openTime = Time.time;
    }

    private void Update()
    {
        if ((animator != null) && Automatic && (Time.time > _openTime))
        {
            animator.SetTrigger("Open");
            _openTime = Time.time + CooldownTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((animator != null) && CheckCollisionType(collision) && !Automatic)
        {
            animator.SetTrigger("Open");
        }
    }

    private bool CheckCollisionType(Collision collision)
    {
        var obstacleFollower = collision.gameObject.GetComponent<ObstacleWaypointFollower>();
        return obstacleFollower != null || collision.gameObject.CompareTag("Player");
    }
}
