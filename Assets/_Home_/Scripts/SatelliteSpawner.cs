using Sirenix.OdinInspector;
using UnityEngine;

public class SatelliteSpawner : MonoBehaviour
{
    [Header("Prefab & Area Settings")]
    [Tooltip("The GameObject prefab you want to clone.")]
    public GameObject prefab;

    [Tooltip("Width (X) and Height (Z) of the rectangular spawn area.")]
    public Vector2 areaSize = new Vector2(50f, 50f);

    [Tooltip("Estimated minimum distance between objects (controls grid cell size).")]
    public float minDistance = 5f;

    [Tooltip("Random seed for reproducible layouts.")]
    public int seed = 1337;

    [Header("Optional Ground Alignment")]
    [Tooltip("Check if you want objects to snap to terrain or ground geometry below them.")]
    public bool alignToGround = false;
    public LayerMask groundLayer;

    [Button]
    public void SpawnSatellites()
    {
        // Clear any existing children before spawning new ones
        ClearSatellites();

        if (prefab == null)
        {
            Debug.LogWarning("Prefab is not assigned on SatelliteSpawner!", this);
            return;
        }

        // Initialize random state for reproducibility
        Random.InitState(seed);

        // Calculate grid rows and columns based on estimated min distance
        int cols = Mathf.FloorToInt(areaSize.x / minDistance);
        int rows = Mathf.FloorToInt(areaSize.y / minDistance);

        if (cols <= 0 || rows <= 0)
        {
            Debug.LogWarning("Area size is too small for the given minimum distance!", this);
            return;
        }

        float cellWidth = areaSize.x / cols;
        float cellHeight = areaSize.y / rows;

        // Center the rect around this GameObject's position
        Vector3 origin = transform.position - new Vector3(areaSize.x / 2f, 0f, areaSize.y / 2f);

        int spawnedCount = 0;

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Jittered grid point generation (The core concept behind Voronoi/cellular feature points)
                float randomX = Random.Range(0f, cellWidth);
                float randomY = Random.Range(0f, cellHeight);

                float posX = origin.x + (x * cellWidth) + randomX;
                float posY = origin.y + (y * cellHeight) + randomY;

                Vector3 spawnPos = new Vector3(posX, posY);

                // Optional: Raycast down to snap to ground/terrain height
                if (alignToGround)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(spawnPos + Vector3.up * 50f, Vector3.down, out hit, 100f, groundLayer))
                    {
                        spawnPos = hit.point;
                    }
                }

                // Instantiate and parent under this spawner object
                GameObject clone = Instantiate(prefab, spawnPos, Quaternion.identity, transform);
                clone.name = $"{prefab.name}_{x}_{y}";
                spawnedCount++;
            }
        }

        Debug.Log($"Successfully spawned {spawnedCount} satellites across the {areaSize.x}x{areaSize.y} area.");
    }

    [Button]
    public void ClearSatellites()
    {
        // Destroy children backwards to prevent index shifting issues in the editor
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}