using UnityEngine;

public class ObstacleWaypointFollower : MonoBehaviour
{
    public ObstacleWaypoint ObstacleWaypoint;
    public AudioSource AudioSource;
    public float Speed = 2f;
    public float DistanceToWaypoint = 0f;
    public float GravityModifier = 0.01f;
    public float Damage = 30f;
    public float CooldownTime = 1f;

    private Rigidbody rb;
    private float nextDamageTime = 0f;

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
        AudioSource.Play();
        AudioSource.volume = 0.7f;
        AudioSource.loop = true;
    }

    void Update()
    {
        rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * GravityModifier);
        if (ObstacleWaypoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, ObstacleWaypoint.transform.position,
                Time.deltaTime * Speed);
            float distance = Vector3.SqrMagnitude(transform.position - ObstacleWaypoint.transform.position);
            if (distance <= DistanceToWaypoint + float.Epsilon)
            {
                ObstacleWaypoint = ObstacleWaypoint.GetNextObstacleWaypoint();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<WaypointFollower>() != null && Time.time > nextDamageTime)
        {
            Events.CollisionWithEnemy(Damage);
            nextDamageTime = Time.time + CooldownTime;
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