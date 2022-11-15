using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Waypoint waypoint;
    public float speed = 1f;
    public bool isActivated = true;
    public bool useCharacterController = true;

    private CharacterController _characterController;

    private void Awake()
    {
        Events.OnTimeRunOut += Deactivate;
        Events.OnRestartGame += Activate;
        Events.OnReachFinish += Deactivate;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnDestroy()
    {
        Events.OnTimeRunOut -= Deactivate;
        Events.OnRestartGame -= Activate;
        Events.OnReachFinish -= Deactivate;
    }

    private void Deactivate()
    {
        isActivated = false;
    }

    private void Activate()
    {
        isActivated = true;
    }

    private void Update()
    {
        if (waypoint is null || !isActivated) return;

        switch (useCharacterController)
        {
            // Moving without CharacterController
            case false:
                transform.position =
                    Vector3.MoveTowards(transform.position, waypoint.transform.position, speed * Time.deltaTime);
                transform.LookAt(waypoint.transform);
                break;

            // Moving using CharacterController
            case true:
                var movement = Vector3.ClampMagnitude(waypoint.transform.position, speed);
                movement *= Time.deltaTime;
                movement = transform.TransformDirection(movement);
                _characterController.Move(movement);
                break;
        }

        // Getting the next waypoint
        if (Vector3.Distance(transform.position, waypoint.transform.position) < 1)
            waypoint = waypoint.GetNextWaypoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        var bacteria = other.gameObject.GetComponent<Bacteria>();
        if (bacteria != null && !bacteria.Attached)
        {
            bacteria.Attached = true;
            bacteria.audioSource.Play();
            var fj = gameObject.AddComponent<FixedJoint>();
            fj.connectedBody = other.gameObject.GetComponent<Rigidbody>();

        }
    }
}