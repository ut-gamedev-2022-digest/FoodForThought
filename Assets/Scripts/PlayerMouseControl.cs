using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    public float mouseSensitivityHorizontal = 10f;
    public float mouseSensitivityVertical = 10f;
    public float clampAngle = 80f;
    public bool isActivated = true;

    private float _verticalRotation = 0f;

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
        var body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    private void Update()
    {
        if (!isActivated) return;
        _verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivityVertical;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -clampAngle, clampAngle);
        // Camera.main.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        
        var delta = Input.GetAxis("Mouse X") * mouseSensitivityHorizontal;
        var horizontalRotation = transform.localEulerAngles.y + delta;
        
        transform.localEulerAngles = new Vector3(_verticalRotation, horizontalRotation, 0);
    }
}