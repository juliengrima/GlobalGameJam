using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;

    public List<GameObject> itemsToSpawn = new List<GameObject>();
    public float spawnDelay = 2f;
    private float bufferDistance = 0.1f;
    private GameObject _lastItemSpawned;

    public bool activateRandomSpawn = false;
    public bool repeatSpawn = false;
    public bool addEvent = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (repeatSpawn)
        {
            StartCoroutine(CoroutinesList.SpawnGameObjectInList(GetComponent<Collider>(), itemsToSpawn, Helpers.GetRandomInt(0, itemsToSpawn.Count), spawnDelay, bufferDistance));
        }
        else
        {
            UIManager.Instance.onGameBegin += SpawnFirstItem;
        }
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
        
        if (addEvent)
        {
            newItem.GetComponent<Item>().OnDestroyEvt.AddListener(SpawnNewItem);
        }
        
        _lastItemSpawned = newItem;
    }

    public void DestroyLast()
    {
        Destroy(_lastItemSpawned);
    }

    void SpawnItem(Collider collider, List<GameObject> list, int index, float distance)
    {

        if (collider != null)
        {
            float minDistance = 5f;

            Vector3 previousSpawnPosition = _lastItemSpawned != null ? _lastItemSpawned.transform.position : Vector3.zero;

            Vector3 randomPoint;
            do
            {
                randomPoint = Helpers.GetRandomPointInBounds(collider.bounds, previousSpawnPosition, minDistance, bufferDistance);

            } while (Physics.OverlapSphereNonAlloc(randomPoint, 5f, null, 1 << 6) > 0);

            Destroy(_lastItemSpawned);
            GameObject newItem = Instantiate(list[index], randomPoint, Quaternion.identity);
            
            if (addEvent)
            {
                newItem.GetComponent<Item>().OnDestroyEvt.AddListener(SpawnNewItem);
            }

            _lastItemSpawned = newItem;
        }
        else
        {
            Debug.LogError("No collider found on the GameObject.");
        }
    }
}
