using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWaypoint : MonoBehaviour
{
    public ObstacleWaypoint Next;
    public float PositionBoundary;
    public Vector3 OriginalPosition;

    private void Awake()
    {
        OriginalPosition = transform.position;
    }

    private Vector3 GetNewPosition(Vector3 originalPosition)
    {
        float newX = OriginalPosition.x + Random.Range(0, PositionBoundary);
        float newY = OriginalPosition.y + Random.Range(0, PositionBoundary);
        float newZ = OriginalPosition.z + Random.Range(0, PositionBoundary);
        return new Vector3(newX, newY, newZ);
    }

    public ObstacleWaypoint GetNextObstacleWaypoint()
    {
        if (Next != null) {
            Next.gameObject.transform.position = GetNewPosition(Next.OriginalPosition);
        }
        return Next;
    }

    private void OnDrawGizmos()
    {
        if (Next == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Next.transform.position);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WaypointFollower>() != null)
        {
            ObstacleSpawner.Instance.Active = false;
        }
    }

}
