using UnityEngine;

public class key_mouse : MonoBehaviour
{
    public float moveSpeed = 30f;     // Velocidad de movimiento
    public float lookSensitivity = 2f; // Sensibilidad del ratón
    public bool keyUpDown = false;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // Bloquea y oculta el cursor para una experiencia más fluida
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        if (keyUpDown)
        {
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            // Asegurar que el movimiento es plano (sin Y)
            Vector3 forward = transform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = transform.right;
            right.y = 0;
            right.Normalize();

            Vector3 move = right * moveX + forward * moveZ;
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        }



        // Rotación con el ratón
        rotationX -= Input.GetAxis("Mouse Y") * lookSensitivity;
        rotationY += Input.GetAxis("Mouse X") * lookSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limita el movimiento vertical

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
