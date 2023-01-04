using UnityEngine;

public class ObstacleWaypointFollower : MonoBehaviour
{
    public ObstacleWaypoint ObstacleWaypoint;
    public AudioSource AudioSource;
    public float Speed = 2f;
    public float DistanceToWaypoint = 0f;
    public float GravityModifier = 0.01f;
    public float DistanceToPlayerToPlaySound = 10f;
    public GameObject Player;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    void Update()
    {
        rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * GravityModifier);
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
        if (Vector3.Distance(transform.position, Player.transform.position) < DistanceToPlayerToPlaySound && !AudioSource.isPlaying)
        {
            AudioSource.Play();
        }
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