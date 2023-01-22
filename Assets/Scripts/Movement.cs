using UnityEngine;

public class Movement : MonoBehaviour
{
    public ObstacleWaypoint ObstacleWaypoint;
    public AudioSource AudioSource;
    public float Speed = 12f;
    public float DistanceToWaypoint = 5f;
    public float DistanceToPlayerToPlaySound = 30f;
    public GameObject Player;


    private void Awake()
    {
        Events.OnPauseGame += OnPauseGame;
        Events.OnResumeGame += OnResumeGame;
        Events.OnLost += OnLost;
    }

    private void OnDestroy()
    {
        Events.OnPauseGame -= OnPauseGame;
        Events.OnResumeGame -= OnResumeGame;
        Events.OnLost -= OnLost;
    }

    private void Start()
    {
        AudioSource.volume = 0.7f;
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
            }
        }
    }

    private void ManageSound()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < DistanceToPlayerToPlaySound && !AudioSource.isPlaying)
        {
            AudioSource.Play();
        }
    }

    private void OnLost(LoseReason _)
    {
        AudioSource.Pause();
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
