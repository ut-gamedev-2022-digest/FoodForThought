using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> nextCandidates = new();
    
    public Waypoint GetNextWaypoint()
    {
        return nextCandidates.Count == 0 ? null : nextCandidates[Random.Range(0, nextCandidates.Count)];
    }

    private void OnDrawGizmos()
    {
        if (nextCandidates.Count == 0) return;

        foreach (var candidate in nextCandidates)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, candidate.transform.position);
        }
    }
}
