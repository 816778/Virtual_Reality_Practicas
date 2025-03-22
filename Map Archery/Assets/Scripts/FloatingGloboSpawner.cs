using UnityEngine;
using System.Collections.Generic;

public class FloatingGloboSpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject globoPrefab;
    public int cubeCount = 30;
    public float minDistance = 4f;

    public float minHeight = 10f;
    public float maxHeight = 70f;

    public float moveSpeed = 10f;

    private List<Vector3> placedPositions = new List<Vector3>();
    private Vector3 cubeSize;

    void Start()
    {
        SpawnFloatingCubes();
    }

    void SpawnFloatingCubes()
    {
        for (int i = 0; i < cubeCount; i++)
        {
            Vector3? pos = GetValidPosition();

            if (pos.HasValue)
            {
                // GameObject cube = Instantiate(globoPrefab, pos.Value, Random.rotation);
                GameObject cube = Instantiate(globoPrefab, pos.Value, Quaternion.identity);
                Renderer rend = cube.GetComponentInChildren<Renderer>();
                cubeSize = rend.bounds.size;

                cube.transform.localScale = cubeSize;

                
                Rigidbody rb = cube.AddComponent<Rigidbody>();
                rb.useGravity = false; // si quieres que floten
                rb.mass = 1f; // puedes ajustar según tamaño
                rb.drag = 0.2f; // fricción en el aire
                rb.angularDrag = 0.5f; // para que no roten por colisiones
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rb.constraints = RigidbodyConstraints.FreezeRotation;

                if (!cube.GetComponent<Collider>())
                    cube.AddComponent<BoxCollider>();

                Vector3 randomDir = Random.onUnitSphere;
                rb.velocity = randomDir * moveSpeed;

                placedPositions.Add(pos.Value);
            }
        }
    }

    Vector3? GetValidPosition()
    {
        int maxAttempts = 100;

        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);
            float y = Random.Range(minHeight, maxHeight);

            Vector3 worldPos = new Vector3(x, y, z) + terrain.transform.position;
            float terrainHeight = terrain.SampleHeight(worldPos);

            if (y - cubeSize.y / 2f > terrainHeight && IsFarFromOthers(worldPos))
                return worldPos;
        }

        return null;
    }

    bool IsFarFromOthers(Vector3 pos)
    {
        foreach (Vector3 placed in placedPositions)
        {
            if (Vector3.Distance(placed, pos) < minDistance)
                return false;
        }
        return true;
    }
}
