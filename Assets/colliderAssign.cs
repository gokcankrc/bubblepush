using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[System.Serializable]
public class PrefabWeight
{
    public GameObject prefab; // The prefab to instantiate
    public float weight;      // The weight (higher values = higher chance)
}

public class colliderAssign : MonoBehaviour
{
    public Tilemap tilemap;                          // Reference to the Tilemap
    public List<PrefabWeight> prefabWeights;         // List of prefabs with weights
    public bool clearExistingObjects = true;         // Option to clear existing objects before spawning

    void Start()
    {
        if (clearExistingObjects)
        {
            ClearSpawnedObjects();
        }

        SpawnPrefabs();
    }

    private void SpawnPrefabs()
    {
        if (tilemap == null)
        {
            Debug.LogError("Tilemap not assigned!");
            return;
        }

        if (prefabWeights.Count == 0)
        {
            Debug.LogError("No prefabs assigned in the list!");
            return;
        }

        // Precalculate total weight for random selection
        float totalWeight = 0f;
        foreach (var prefabWeight in prefabWeights)
        {
            totalWeight += prefabWeight.weight;
        }

        // Iterate through all positions in the tilemap
        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null)
            {
                // Convert the tile position to world space
                Vector3 worldPosition = tilemap.CellToWorld(position) + tilemap.tileAnchor;

                // Choose a prefab based on weights
                GameObject prefabToInstantiate = ChoosePrefabBasedOnWeight(totalWeight);
                if (prefabToInstantiate != null)
                {
                    // Instantiate the prefab at the tile's position
                    GameObject instance = Instantiate(prefabToInstantiate, worldPosition, Quaternion.identity, transform);
                    instance.name = $"Prefab_{position}";
                }
            }
        }
    }

    private GameObject ChoosePrefabBasedOnWeight(float totalWeight)
    {
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var prefabWeight in prefabWeights)
        {
            cumulativeWeight += prefabWeight.weight;
            if (randomValue <= cumulativeWeight)
            {
                return prefabWeight.prefab;
            }
        }

        return null; // Should not reach here if weights are set correctly
    }

    private void ClearSpawnedObjects()
    {
        // Destroy all child objects of this GameObject
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
