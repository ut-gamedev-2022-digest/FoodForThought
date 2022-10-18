using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    public float mouseSensitivityHorizontal = 10f;
    public float mouseSensitivityVertical = 10f;
    public float clampAngle = 80f;
    
    private float _verticalRotation = 0f;
    
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
        _verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivityVertical;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -clampAngle, clampAngle);
        // Camera.main.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        
        var delta = Input.GetAxis("Mouse X") * mouseSensitivityHorizontal;
        var horizontalRotation = transform.localEulerAngles.y + delta;
        
        transform.localEulerAngles = new Vector3(_verticalRotation, horizontalRotation, 0);
    }
}