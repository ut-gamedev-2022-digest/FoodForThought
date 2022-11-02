using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWaypointFollower : MonoBehaviour
{
    public ObstacleWaypoint ObstacleWaypoint;
    public float Speed = 2f;

    void Update()
    {
        if (ObstacleWaypoint == null)
        {
            DestinationReached();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, ObstacleWaypoint.transform.position, Time.deltaTime * Speed);
            float distance = Vector3.SqrMagnitude(transform.position - ObstacleWaypoint.transform.position);
            if (distance <= float.Epsilon)
            {
                ObstacleWaypoint = ObstacleWaypoint.GetNextObstacleWaypoint();
            }
        }
    }

    private void DestinationReached()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<WaypointFollower>() != null)
        {
            Events.TimeRunOut();
        }
    }
}
