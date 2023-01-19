using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWaypoint : MonoBehaviour
{
    public ObstacleWaypoint Next;
    public float PositionBoundary;
    public Vector3 OriginalPosition;
    public bool ChangePosition = false;

    private void Awake()
    {
        OriginalPosition = transform.position;
        ChangePosition = false;
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
        if (Next != null && ChangePosition) {
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

}
