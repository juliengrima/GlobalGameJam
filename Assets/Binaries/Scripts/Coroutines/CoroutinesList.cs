using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class CoroutinesList : MonoBehaviour
    {
        #region Coroutines
        IEnumerator EndCoroutine()
        {
            throw new NotImplementedException();
        }

        internal static IEnumerator SpawnGameObjectInList(Collider collider, List<GameObject> list, int index, float delay)
        {
            while (true)
            {
                if (collider != null)
                {
                    Vector3 spawnPoint = Helpers.GetRandomPointInBounds(collider.bounds);
                    
                    GameObject newItem = Instantiate(list[index], spawnPoint, Quaternion.identity);
                    
                    GameObject lastItemSpawned = newItem;

                    Destroy(lastItemSpawned, 0.8f);
                }
                else
                {
                    Debug.LogError("No collider found on the GameObject.");
                }

                yield return new WaitForSeconds(delay);
            }
            
        }
        #endregion
    }
}

