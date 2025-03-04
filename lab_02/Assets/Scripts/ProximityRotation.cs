using UnityEngine;

public class ProximityRotation : MonoBehaviour
{
    public Transform player;  // Asigna el jugador en el Inspector
    public float maxSpeed = 1000f;  // Velocidad máxima de giro
    public float minSpeed = 0f;   // Velocidad mínima
    public float detectionRange = 10f;  // Distancia máxima para acelerar la rotación
    public float teleportRange = 4f;
    private float currentSpeed;

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned in " + gameObject.name);
            return;
        }

        // Calcular la distancia al jugador
        float distance = Vector3.Distance(transform.position, player.position);

        // Ajustar la velocidad de rotación con base en la distancia
        float speedFactor = Mathf.Lerp(maxSpeed, minSpeed, distance/(detectionRange-teleportRange));
        currentSpeed = Mathf.Clamp(speedFactor, minSpeed, maxSpeed);

        float speedFactor2 = Mathf.InverseLerp(detectionRange-teleportRange, 0, distance);
        
        float newXRotation = Mathf.Lerp(-90f, -80f, speedFactor2);
        transform.rotation = Quaternion.Euler(newXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // Rotar sobre el eje Y
        transform.Rotate(Vector3.up, currentSpeed * Time.deltaTime, Space.World);
    }
}