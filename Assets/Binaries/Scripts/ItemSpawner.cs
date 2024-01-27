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

    int GetRandomGO()
    {
        int rand = UnityEngine.Random.Range(0, itemsToSpawn.Count);
        return rand;
    }

}
