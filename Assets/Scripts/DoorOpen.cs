using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((animator != null) && CheckCollisionType(collision))
        {
            animator.SetTrigger("Open");
        }
    }

    private bool CheckCollisionType(Collision collision)
    {
        var obstacleFollower = collision.gameObject.GetComponent<ObstacleWaypointFollower>();
        var timer = collision.gameObject.GetComponent<Timer>();
        return obstacleFollower != null || timer != null;
    }
}
