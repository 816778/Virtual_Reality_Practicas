using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -5);
    public float mouseSensitivity = 100f;
    public float distance = 5f;
    public float height = 2f;
    public float rotationSmoothTime = 0.12f;

    private float yaw;   // Horizontal
    private float pitch; // Vertical

    void Update()
    {
        // Mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Limita la rotación vertical
        pitch = Mathf.Clamp(pitch, -30f, 60f);
    }

    void LateUpdate()
    {
        // Calcula la rotación y posición
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = target.position - rotation * Vector3.forward * distance + Vector3.up * height;

        transform.position = targetPosition;
        transform.LookAt(target.position + Vector3.up * height);
    }
}
