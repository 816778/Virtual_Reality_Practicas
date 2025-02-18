using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSpawner : MonoBehaviour
{
    // Properties that can be changed from the Inspetor tab
    public GameObject ItemToSpawn; // Item to be spawned
    public Vector3 center; // Center of the cube to spawn
    public Vector3 size; // Size of the cube to spawn
    public Transform playerCamera;
    public TextMeshProUGUI spawnTimeText;

    private float spawnCooldown = 1f; // Time between spawns
    private float timeSinceLastSpawn = 0f; // Last time an item was spawned
    // Start is called before the first frame update
    void Start()
    {
        SpawnItem();
    }
    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (spawnTimeText != null)
        {
            spawnTimeText.text = $"Spawn Time: {timeSinceLastSpawn:F2} s";
        }

        if (Input.GetKey(KeyCode.Q) && timeSinceLastSpawn >= spawnCooldown && IsPlayerInsideRoom())
        {
            SpawnItem();
            timeSinceLastSpawn = 0f; // Reinicia el temporizador
        }
    }

    public void SpawnItem()
    {
        // Position to spawn
        Vector3 pos = center + new Vector3(Random.Range(-size.x/2, size.x/2),
        Random.Range(-size.y/2, size.y/2),
        Random.Range(-size.z/2, size.z/2));
        // Instantiate the item
        Instantiate(ItemToSpawn, pos, Quaternion.identity);
    }

    private bool IsPlayerInsideRoom()
    {
        Vector3 playerPos = playerCamera.position;

        return (playerPos.x > center.x - size.x / 2 && playerPos.x < center.x + size.x / 2 &&
                playerPos.y > center.y - size.y / 2 && playerPos.y < center.y + size.y / 2 &&
                playerPos.z > center.z - size.z / 2 && playerPos.z < center.z + size.z / 2);
    }

    // Debug funcion. In the Scene tab you can see a Red Box, which is the
    // volume where the object is going to be spawned.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}