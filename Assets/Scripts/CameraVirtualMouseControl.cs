using Cinemachine;
using UnityEngine;

public class CameraVirtualMouseControl : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private CinemachineTransposer _transposer;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        var wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel != 0)
        {
            var newZ = Mathf.Clamp(_transposer.m_FollowOffset.z + wheel * 10, -45, 0);
            _transposer.m_FollowOffset.z = newZ;
        }
        
        var mouseY = Input.GetAxis("Mouse Y") * 10;
        if (mouseY != 0)
        {
            var newY = Mathf.Clamp(_transposer.m_FollowOffset.y + mouseY, 5, 20);
            _transposer.m_FollowOffset.y = newY;
        }
    }
}