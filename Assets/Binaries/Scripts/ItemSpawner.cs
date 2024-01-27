using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Unity.VisualScripting;
using System;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemsToSpawn = new List<GameObject>();
    public float spawnDelay = 2f;
    
    private void Start()
    {
        Collider collider = GetComponent<Collider>();

        StartCoroutine(CoroutinesList.SpawnGameObjectInList(collider, itemsToSpawn, GetRandomGO(), spawnDelay));
    }

    void SpawnItem(Collider collider, List<GameObject> list, int index, float delay)
    {
        if (collider != null)
        {
            Vector3 previousSpawnPosition = list[index].transform.position;
            float minDistance = 10f; // Minimum distance from the previous spawn position

            Vector3 randomPoint = Helpers.GetRandomPointInBounds(collider.bounds, previousSpawnPosition, minDistance);

            GameObject newItem = Instantiate(list[index], randomPoint, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No collider found on the GameObject.");
        }
    }

    int GetRandomGO()
    {
        int rand = UnityEngine.Random.Range(0, itemsToSpawn.Count);
        return rand;
    }

}
