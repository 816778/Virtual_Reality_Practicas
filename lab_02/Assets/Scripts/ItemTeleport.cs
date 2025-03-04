using UnityEngine;
using TMPro;

public class ItemTeleport : MonoBehaviour
{
    public GameObject ItemToTeleport; // Referencia al objeto que se moverá
    public Vector3 center; // Centro del área donde puede teletransportarse
    public Vector3 size; // Tamaño del área de teletransporte

    public Transform playerCamera;
    public float teleportDistance = 2f;

    void Start()
    {
    }

    void Update()
    {
        if (IsPlayerCloseObject()) 
        {
            TeleportItem(); // Teletransporta el objeto cuando el jugador se acerca
        }
    }

    public void TeleportItem()
    {
        if (ItemToTeleport == null)
        {
            Debug.LogWarning("No ItemToTeleport assigned!");
            return;
        }

        // Nueva posición aleatoria dentro del área
        Vector3 newPos = center + new Vector3(
            Random.Range(-size.x / 2, size.x / 2),
            Random.Range(-size.y / 2, size.y / 2),
            Random.Range(-size.z / 2, size.z / 2)
        );

        // Mover el objeto en lugar de instanciar uno nuevo
        ItemToTeleport.transform.position = newPos;
    }

    private bool IsPlayerCloseObject()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("No PlayerCamera assigned!");
            return false;
        }

        float distance = Vector3.Distance(playerCamera.position, ItemToTeleport.transform.position);
        
        // Retorna verdadero si el jugador está lo suficientemente cerca
        return distance <= teleportDistance;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
