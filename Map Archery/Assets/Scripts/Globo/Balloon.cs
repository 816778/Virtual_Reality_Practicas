using UnityEngine;
using System.Collections.Generic;

public class  Balloon: MonoBehaviour
{
    public GameObject balloonPrefab;
    public int balloonCount = 10;
    public Vector3 balloonSize = new Vector3(2, 4, 2);
    public float minDistance = 3f;
    public float minHeight = 10f;
    public float maxHeight = 50f;
    public Terrain terrain;

    protected List<Vector3> placedPositions = new List<Vector3>();
    protected List<Bounds> placedBounds = new List<Bounds>();


    protected virtual void Start()
    {
        SpawnBalloons();
    }

    protected virtual void SpawnBalloons()
    {
        for (int i = 0; i < balloonCount; i++)
        {
            Bounds? validBounds = GetValidBounds();

            if (validBounds.HasValue)
            {
                Vector3 spawnPos = validBounds.Value.center;
                GameObject balloon = Instantiate(balloonPrefab, spawnPos, Quaternion.identity);
                balloon.transform.localScale = balloonSize;
                placedBounds.Add(GetFullBounds(balloon));
                //Debug.Log("Balloon " + i + " created at " + spawnPos);
            }
        }
    }

    protected Bounds? GetValidBounds()
    {
        int maxAttempts = 100;

        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);
            float y = Random.Range(minHeight, maxHeight);
            float terrainY = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.transform.position.y;

            Vector3 worldPos = new Vector3(x, y, z);

            // Simulamos instanciación para obtener los bounds
            GameObject temp = Instantiate(balloonPrefab, worldPos, Quaternion.identity);
            temp.transform.localScale = balloonSize;
            Bounds b = GetFullBounds(temp);
            DestroyImmediate(temp);

            if (y - b.extents.y > terrainY && IsFarFromOthers(b))
            {
                return b;
            }
        }

        return null;
    }

    protected bool IsFarFromOthers(Bounds newBounds)
    {
        foreach (Bounds existing in placedBounds)
        {
            if (existing.Intersects(newBounds))
                return false;
        }
        return true;
    }


    protected Bounds GetFullBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return new Bounds(obj.transform.position, Vector3.zero);

        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }
        return bounds;
    }
}


