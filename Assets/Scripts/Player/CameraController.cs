
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float orbitSensitivity = 0.01f; // Sensitivity for swipe-based orbiting
    public float zoomSpeed = 0.02f; // Speed for pinch-to-zoom
    private float zoomAmount = 8f; // Default zoom distance (adjustable)
    public float orbitSensitivityX = 0.01f; // Horizontal swipe sensitivity
    public float orbitSensitivityY = 0.001f;
    private float targetYAxisValue;
    public float yAxisSmoothing = 3f;
    public Vector3 CameraOffset;
    public CinemachineCameraOffset cameraOffset;

    void Start()
    {
        // Initialize target Y-axis value
        targetYAxisValue = freeLookCamera.m_YAxis.Value;
        cameraOffset = freeLookCamera.GetComponent<CinemachineCameraOffset>();

    }

    public void Update()
    {
        UpdateCameraOffset();
    }

    public void UpdatePosAndRotatetion(Vector2 input )
    {
        Vector2 swipe = input;
        freeLookCamera.m_XAxis.Value += swipe.x * orbitSensitivity; // Horizontal rotation
        targetYAxisValue += swipe.y * orbitSensitivityY;
        targetYAxisValue = Mathf.Clamp01(targetYAxisValue); // Keep within [0,1] for FreeLook Y-axis
        freeLookCamera.m_YAxis.Value = Mathf.Lerp(freeLookCamera.m_YAxis.Value, targetYAxisValue, Time.deltaTime * yAxisSmoothing);
    }

    private Vector3 cameraOffsetVelocity;
    public Vector3 cameraOffsetTarget;

    public Vector3 CameraOffsetTarget
    {
        get => cameraOffsetTarget;
        set => cameraOffsetTarget = value;
    }
    public void UpdateCameraOffset()
    {
        print(cameraOffsetTarget);
        cameraOffset.m_Offset = Vector3.SmoothDamp(cameraOffset.m_Offset, cameraOffsetTarget, ref cameraOffsetVelocity, 1);
    }
}