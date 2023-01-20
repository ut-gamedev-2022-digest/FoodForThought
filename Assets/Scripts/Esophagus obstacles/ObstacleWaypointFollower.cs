using UnityEngine;

public class ObstacleWaypointFollower : MonoBehaviour
{
    public ObstacleWaypoint ObstacleWaypoint;
    public AudioSource AudioSource;
    public float Speed = 2f;
    public float DistanceToWaypoint = 0f;
    public float DistanceToPlayerToPlaySound = 10f;

    private void Awake()
    {
        Events.OnPauseGame += OnPauseGame;
        Events.OnResumeGame += OnResumeGame;
    }

    private void OnDestroy()
    {
        Events.OnPauseGame -= OnPauseGame;
        Events.OnResumeGame -= OnResumeGame;
    }

    private void Start()
    {
        AudioSource.volume = 0.7f;
        ObstacleWaypoint = LoadLevel.Instance.FirstObstacleWaypoint;
    }

    void Update()
    {
        if (ObstacleWaypoint != null)
        {
            ManageSound();
            transform.position = Vector3.MoveTowards(transform.position, ObstacleWaypoint.transform.position,
                Time.deltaTime * Speed);
            float distance = Vector3.SqrMagnitude(transform.position - ObstacleWaypoint.transform.position);
            if (distance <= DistanceToWaypoint + float.Epsilon)
            {
                ObstacleWaypoint = ObstacleWaypoint.GetNextObstacleWaypoint();
                if (ObstacleWaypoint == null)
                {
                    int destroyTrigger = Random.Range(0, 2);
                    if (destroyTrigger == 1)
                    {
                        Destroy(this);
                    }
                }
            }
        }
    }

    private void ManageSound()
    {
        
         AudioSource.Play();
        
    }
    
    private void OnPauseGame()
    {
        AudioSource.Pause();
    }
    
    private void OnResumeGame()
    {
        AudioSource.UnPause();
    }
}