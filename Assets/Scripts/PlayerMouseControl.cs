using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    public float mouseSensitivity = 10f;
    public bool isActivated = true;

    private float _verticalRotation = 0f;
    private float _horizontalRotation = 0f;

    private void Awake()
    {
        Events.OnTimeRunOut += Deactivate;
        Events.OnRestartGame += Activate;
        Events.OnReachFinish += Deactivate;
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

    private void Start()
    {
        var body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    private void Update()
    {
        if (!isActivated) return;
        
        _verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        _horizontalRotation -= Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.localEulerAngles = new Vector3(_verticalRotation, _horizontalRotation, 0);
    }
}