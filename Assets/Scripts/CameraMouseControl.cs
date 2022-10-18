using Cinemachine;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    private CinemachineFreeLook _camera;

    private void Start()
    {
        _camera = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        var wheel = Input.GetAxis("Mouse ScrollWheel");
        var delta = _camera.m_YAxis.Value + wheel;
        _camera.m_YAxis.Value = Mathf.Clamp(delta, 0.2f, 1f);
    }
}