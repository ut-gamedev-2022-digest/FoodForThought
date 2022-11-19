using System;
using UnityEngine;

public class PlayerKeyboardControl : MonoBehaviour
{
    public bool isActivated = true;
    public float speed = 5f;
    public float gravity = -9.8f;
    public bool useCharacterController = true;

    private CharacterController _characterController;
    private Rigidbody _rigidbody;
    private Vector3 _movement;

    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private bool _isGravityEnabled = true;

    private bool isPaused = false;
    public GameObject PausePanel;

    private void Awake()
    {
        Events.OnLost += Deactivate;
        Events.OnRestartGame += Activate;
        Events.OnReachFinish += Deactivate;
    }

    private void OnDestroy()
    {
        Events.OnLost -= Deactivate;
        Events.OnRestartGame -= Activate;
        Events.OnReachFinish -= Deactivate;
    }

    private void Deactivate(LoseReason loseReason)
    {
        isActivated = false;
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
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        PausePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isActivated) return;

        // WASD movements
        var deltaX = Input.GetAxis("Horizontal") * speed;
        var deltaZ = Input.GetAxis("Vertical") * speed;
        var movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        if (_isGravityEnabled) movement.y = gravity;
        movement *= Time.deltaTime;
        _movement = movement;

        // Walking animation
        _animator.SetFloat(Speed, Mathf.Abs(deltaX) + Mathf.Abs(deltaZ));

        // Restart screen on Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;
                PausePanel.SetActive(true);
            }
            else 
            {
                Time.timeScale = 1f;
                isPaused = false;
                PausePanel.SetActive(false);
            }
            //Events.TimeRunOut();
        }

        if (!isActivated) return;

        switch (useCharacterController)
        {
            case true:
                CharacterControllerMovement();
                break;
            case false:
                RigidbodyMovement();
                break;
        }
    }

    private void CharacterControllerMovement()
    {
        _movement = transform.TransformDirection(_movement);
        _characterController.Move(_movement);
        _movement = Vector3.zero;
    }

    private void RigidbodyMovement()
    {
        _movement = transform.TransformDirection(_movement);
        _rigidbody.transform.position += _movement;
        _movement = Vector3.zero;
    }

    public void EnableGravity()
    {
        _isGravityEnabled = true;
    }

    public void DisableGravity()
    {
        _isGravityEnabled = false;
    }
}