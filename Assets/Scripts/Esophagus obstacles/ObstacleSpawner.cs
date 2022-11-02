using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;
    public bool Active;
    public float TimeBetweenSpawns;
    public GameObject ObstaclePrefab;

    private float nextSpawnTime;
    private ObstacleWaypoint obstacleWaypoint;

    private void Awake()
    {
        Instance = this;
        Active = false;
        obstacleWaypoint = GetComponent<ObstacleWaypoint>();
    }
    private void Update()
    {
        if (Active)
        {
            if (Time.time > nextSpawnTime)
            {
                SpawnObstacle();
            }
        }    
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = GameObject.Instantiate(ObstaclePrefab, transform.position, Quaternion.identity, null);
        ObstacleWaypointFollower obstacleWaypointFollower = obstacle.GetComponent<ObstacleWaypointFollower>();
        obstacleWaypointFollower.ObstacleWaypoint = obstacleWaypoint;

        nextSpawnTime = Time.time + TimeBetweenSpawns;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WaypointFollower>() != null)
        {
            Active = true;
        }
    }
}
