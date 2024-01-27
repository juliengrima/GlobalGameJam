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
    private float bufferDistance = 0.1f;

    private Collider _collider = null;
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
        
        //For testing item spawning
        //StartCoroutine(CoroutinesList.SpawnGameObjectInList(GetComponent<Collider>(), itemsToSpawn, Helpers.GetRandomInt(0, itemsToSpawn.Count), spawnDelay, bufferDistance));
        
        SpawnItem();
    }

    public void SpawnItem()
    {
        SpawnItem(_collider, itemsToSpawn, Helpers.GetRandomInt(0, itemsToSpawn.Count), bufferDistance);
    }

    private void SpawnItem(Collider collider, List<GameObject> list, int index, float distance)
    {
        if (collider != null)
        {
            Vector3 previousSpawnPosition = list[index].transform.position;
            float minDistance = 10f; // Minimum distance from the previous spawn position

            Vector3 randomPoint = Helpers.GetRandomPointInBounds(collider.bounds, previousSpawnPosition, minDistance, distance);

            GameObject newItem = Instantiate(list[index], randomPoint, Quaternion.identity);

            Item itemController = newItem.GetComponent<Item>();
            itemController.onEat += SpawnItem;
        }
        else
        {
            Debug.LogError("No collider found on the GameObject.");
        }
    }
}
