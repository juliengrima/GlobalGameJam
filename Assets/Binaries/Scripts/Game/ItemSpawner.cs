using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemsToSpawn = new List<GameObject>();
    public float spawnDelay = 2f;
    private float bufferDistance = 0.1f;

    

    private void Start()
    {
        //For testing item spawning
        //StartCoroutine(CoroutinesList.SpawnGameObjectInList(GetComponent<Collider>(), itemsToSpawn, Helpers.GetRandomInt(0, itemsToSpawn.Count), spawnDelay, bufferDistance
        UIManager.Instance.onGameBegin += SpawnItem;
    }

    public void SpawnItem()
    {
        SpawnItem(GetComponent<Collider>(), itemsToSpawn, Helpers.GetRandomInt(0, itemsToSpawn.Count), bufferDistance);
    }

    private void SpawnItem(Collider collider, List<GameObject> list, int index, float distance)
    {
        GameObject lastItemSpawned = null;

        if (collider != null)
        {
            float minDistance = 5f;

            Vector3 previousSpawnPosition = lastItemSpawned != null ? lastItemSpawned.transform.position : Vector3.zero;

            Vector3 randomPoint = Helpers.GetRandomPointInBounds(collider.bounds, previousSpawnPosition, minDistance, bufferDistance);

            GameObject newItem = Instantiate(list[index], Vector3.zero, Quaternion.identity);

            lastItemSpawned = newItem;
        }
        else
        {
            Debug.LogError("No collider found on the GameObject.");
        }
    }
}
