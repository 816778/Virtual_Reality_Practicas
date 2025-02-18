using UnityEngine;

public class PillarDestroyer : MonoBehaviour
{
    public float rayDistance = 20f; // Distancia del raycast
    public bool makeInvisible = false; // True para hacer invisible, False para destruir

    void Update()
    {
        // Dirección de la cámara
        Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistance;

        // Dibuja un rayo visible en la escena (modo Scene para depuración)
        Debug.DrawRay(transform.position, forward, Color.red);

        // Raycast desde la posición de la cámara
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance))
        {
            // Comprueba si el objeto tiene la etiqueta "Pillar"
            if (hit.collider.CompareTag("Pillar"))
            {
                if (makeInvisible)
                {
                    // Hace invisible el pilar
                    Renderer pillarRenderer = hit.collider.GetComponent<Renderer>();
                    if (pillarRenderer != null)
                    {
                        pillarRenderer.enabled = false; // Oculta el pilar
                    }
                }
                else
                {
                    // Destruye el pilar
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
