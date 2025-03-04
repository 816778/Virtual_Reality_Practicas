using UnityEngine;

public class PillarDestroyer : MonoBehaviour
{
    public float rayDistance = 30f; // Distancia del raycast
    public bool makeInvisible = false; // True para hacer invisible, False para destruir

    void Update()
    {
        // Dirección cámara
        Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistance;
        Debug.DrawRay(transform.position, forward, Color.red);

        // Raycast desde la posición de la cámara
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("Pillar"))
            {
                if (makeInvisible)
                {
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
