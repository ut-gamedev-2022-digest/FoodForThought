using UnityEngine;

public class PlayerKeyboardControl : MonoBehaviour
{
    public bool isActivated = true;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpSpeed = 5f;
    
    private CharacterController _characterController;

    private void Awake()
    {
        Events.OnTimeRunOut += Deactivate;
        Events.OnRestartGame += Activate;
    }

    private void OnDestroy()
    {
        Events.OnTimeRunOut -= Deactivate;
        Events.OnRestartGame -= Activate;
    }

    private void Deactivate()
    {
        isActivated = false;
    }

    private void Activate()
    {
        isActivated = true;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!isActivated) return;

        var deltaX = Input.GetAxis("Horizontal") * speed;
        var deltaZ = Input.GetAxis("Vertical") * speed;
        
        var movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }
}