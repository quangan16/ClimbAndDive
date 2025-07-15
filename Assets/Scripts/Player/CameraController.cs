// using UnityEngine;
//
// public class CameraController : MonoBehaviour
// {
//     [Header("Target")]
//     [SerializeField] private Transform followTarget;
//
//     [Header("Camera Offset")]
//     [SerializeField] private Vector3 offset = new(0, 0.5f, -4.5f);
//     [SerializeField] private float moveSmoothTime = 0.05f;
//
//     [Header("Rotation")]
//     [SerializeField] private float yawRotateSpeed = 0.5f;
//     [SerializeField] private float pitchRotateSpeed = 0.5f;
//     [SerializeField] private float minPitch = -40f;
//     [SerializeField] private float maxPitch = 80f;
//
//     [Header("References")]
//     [SerializeField] private Camera cam;
//
//     private float yaw = 0f;
//     private float pitch = 10f;
//     private Vector3 velocity;
//
//     void Start()
//     {
//         if (!cam) cam = GetComponentInChildren<Camera>();
//
//         // yaw = transform.eulerAngles.y;
//         // pitch = cam.transform.localEulerAngles.x;
//         transform.forward = followTarget.forward;
//         UpdateRigPosition();
//
//         UpdateCameraOffsetAndPitch();
//     }
//
//     void LateUpdate()
//     {
//         HandleRotation();
//         UpdateRigPosition();
//         UpdateCameraOffsetAndPitch();
//     }
//
//     private void HandleRotation()
//     {
//         Vector2 swipe = InputManager.Instance.CameraDirection;
//
//         if (swipe.magnitude >= 0.1f)
//         {
//             yaw -= swipe.x * yawRotateSpeed;
//             pitch -= swipe.y * pitchRotateSpeed;
//             pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
//         }
//
//         // Apply yaw to rig (Y-axis only)
//         transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
//     }
//
//     private void UpdateRigPosition()
//     {
//         if (!followTarget) return;
//
//         Vector3 targetPos = followTarget.position;
//         transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, moveSmoothTime);
//     }
//
//     private void UpdateCameraOffsetAndPitch()
//     {
//         if (!cam) return;
//
//         // Set camera local position and rotation
//         cam.transform.localPosition = offset;
//         // cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
//     }
// }

using UnityEngine;

// public class CameraController : MonoBehaviour
// {
//     public Transform target;       // The player
//     public Vector3 offset = new Vector3(0, 5, -7);
//     public float smoothSpeed = 10f;
//
//     public float rotationSpeed = 5f;
//     private float yaw = 0f;
//     private float pitch = 10f;// Smoothing factor
//
//     [SerializeField] private float yawRotateSpeed = 0.5f;
//      [SerializeField] private float pitchRotateSpeed = 0.5f;
//      [SerializeField] private float minPitch = -40f;
//      [SerializeField] private float maxPitch = 80f;
//
//      void Start()
//      {
//          if (target != null)
//          {
//              yaw = target.eulerAngles.y;
//          }
//      }
//     void LateUpdate()
//     {
//         HandleRotation();
//     }
//
//     public void HandleRotation()
//     {
//         if (target == null) return;
//         Vector2 swipe = InputManager.Instance.CameraDirection;
//
//         if (swipe.magnitude >= 0.1f)
//         {
//             yaw += swipe.x * yawRotateSpeed;
//              pitch -= swipe.y * pitchRotateSpeed;
//              pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
//         }
//
//
//         Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
//         Vector3 desiredPosition = target.position
//                                   + rotation * offset;
//
//         transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//         transform.LookAt(target.position + Vector3.up * 13);
//     }
// }

using Cinemachine;

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


    void Start()
    {
        // Initialize target Y-axis value
        targetYAxisValue = freeLookCamera.m_YAxis.Value;


    }

    public void UpdatePosAndRotatetion(Vector2 input )
    {
        Vector2 swipe = input;
        freeLookCamera.m_XAxis.Value += swipe.x * orbitSensitivity; // Horizontal rotation
        targetYAxisValue += swipe.y * orbitSensitivityY;
        targetYAxisValue = Mathf.Clamp01(targetYAxisValue); // Keep within [0,1] for FreeLook Y-axis
        freeLookCamera.m_YAxis.Value = Mathf.Lerp(freeLookCamera.m_YAxis.Value, targetYAxisValue, Time.deltaTime * yAxisSmoothing);
    }
}