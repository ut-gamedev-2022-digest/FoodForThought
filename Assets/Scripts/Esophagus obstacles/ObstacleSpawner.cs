using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;
    public bool Active;
    public float TimeBetweenSpawns;
    public List<GameObject> ObstaclePrefabs;

    private float nextSpawnTime;
    private ObstacleWaypoint obstacleWaypoint;

    private void Awake()
    {
        Instance = this;
        Active = false;
        obstacleWaypoint = GetComponent<ObstacleWaypoint>();
        CheckLaunchMode();
    }

    private void CheckLaunchMode()
    {
        int mode = PlayerPrefs.GetInt("LaunchMode", 1);
        if (mode == 0){
            gameObject.SetActive(false);
        }
        else if (mode == 1)
        {
            gameObject.SetActive(true);
        }
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

    private GameObject ChooseObstaclePrefab()
    {
        int idx = Random.Range(0, ObstaclePrefabs.Count);
        return ObstaclePrefabs[idx];
    }

    private void SpawnObstacle()
    {
        if (ObstaclePrefabs.Count > 0)
        {
            GameObject obstacle = GameObject.Instantiate(ChooseObstaclePrefab(), transform.position, Quaternion.identity, null);
            ObstacleWaypointFollower obstacleWaypointFollower = obstacle.GetComponent<ObstacleWaypointFollower>();
            obstacleWaypointFollower.ObstacleWaypoint = obstacleWaypoint;

            nextSpawnTime = Time.time + TimeBetweenSpawns;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Active = true;
            nextSpawnTime = Time.time + TimeBetweenSpawns;
        }
    }
}
