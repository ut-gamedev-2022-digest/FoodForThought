using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Waypoint waypoint;
    public float speed = 1f;
    public bool isActivated = true;
    
    private void Update()
    {
        if (waypoint is null || !isActivated) return;

        transform.position =
            Vector3.MoveTowards(transform.position, waypoint.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoint.transform.position) < float.Epsilon)
        {
            waypoint = waypoint.GetNextWaypoint();
        }
    }
}