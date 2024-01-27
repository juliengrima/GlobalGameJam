using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemsToSpawn = new List<GameObject>();
    public float spawnDelay = 2f;
    private float bufferDistance = 0.1f;
    private GameObject _lastItemSpawned;

    private void Start()
    {
        //For testing item spawning
        //StartCoroutine(CoroutinesList.SpawnGameObjectInList(GetComponent<Collider>(), itemsToSpawn, Helpers.GetRandomInt(0, itemsToSpawn.Count), spawnDelay, bufferDistance
        UIManager.Instance.onGameBegin += SpawnFirstItem;

    }

    
    public void SpawnFirstItem()
    {
        SpawnFirstItem(itemsToSpawn, Helpers.GetRandomInt(0, itemsToSpawn.Count));
    }

    public void SpawnNewItem()
    {
        SpawnItem(GetComponent<Collider>(), itemsToSpawn, Helpers.GetRandomInt(0, itemsToSpawn.Count), bufferDistance);
    }

    void SpawnFirstItem(List<GameObject> list, int index)
    {
        GameObject newItem = Instantiate(list[index], Vector3.zero, Quaternion.identity);

        newItem.GetComponent<Item>().OnDestroyEvt.AddListener(SpawnNewItem);
        _lastItemSpawned = newItem;
    }

    void SpawnItem(Collider collider, List<GameObject> list, int index, float distance)
    {
        GameObject _lastItemSpawned = null;

        if (collider != null)
        {
            float minDistance = 5f;

            Vector3 previousSpawnPosition = _lastItemSpawned != null ? _lastItemSpawned.transform.position : Vector3.zero;

            Vector3 randomPoint = Helpers.GetRandomPointInBounds(collider.bounds, previousSpawnPosition, minDistance, bufferDistance);

            GameObject newItem = Instantiate(list[index], randomPoint, Quaternion.identity);

            newItem.GetComponent<Item>().OnDestroyEvt.AddListener(SpawnNewItem);

            _lastItemSpawned = newItem;
        }
        else
        {
            Debug.LogError("No collider found on the GameObject.");
        }
    }
}
