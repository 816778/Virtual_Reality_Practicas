using UnityEngine;
using System.Collections.Generic;

public class TerrainCubeSpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject cubePrefab;
    public int cubeCount = 20;
    public Vector3 cubeSize = new Vector3(2, 2, 2);
    public float minDistance = 3f;
    public float maxSlope = 20f;
    public float minHeight = 10f;
    public float maxHeight = 50f;


    private List<Vector3> placedPositions = new List<Vector3>();

    void Start()
    {
        SpawnCubes();
    }

    void SpawnCubes()
    {
        for (int i = 0; i < cubeCount; i++)
        {
            Vector3? pos = GetValidPosition();

            if (pos.HasValue)
            {
                GameObject cube = Instantiate(cubePrefab, pos.Value, Quaternion.identity);
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
            // float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.transform.position.y;
            float y = Random.Range(minHeight, maxHeight);
            float terrainY = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.transform.position.y;

            Vector3 worldPos = new Vector3(x, y + cubeSize.y / 2f, z);

            if (y - cubeSize.y / 2f > terrainY && IsFlatEnough(x, z) && IsFarFromOthers(worldPos))
            {
                return worldPos;
            }
        }

        return null;
    }

    bool IsFlatEnough(float x, float z)
    {
        Vector3 normal = terrain.terrainData.GetInterpolatedNormal(
            x / terrain.terrainData.size.x,
            z / terrain.terrainData.size.z);
        float slope = Vector3.Angle(normal, Vector3.up);
        return slope < maxSlope;
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
