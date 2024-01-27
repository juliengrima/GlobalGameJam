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
            GameObject lastItemSpawned = null;

            while (true)
            {
                if (collider != null)
                {
                    Vector3 previousSpawnPosition = lastItemSpawned != null ? lastItemSpawned.transform.position : Vector3.zero;
                    float minDistance = 5f;

                    Vector3 randomPoint = Helpers.GetRandomPointInBounds(collider.bounds, previousSpawnPosition, minDistance);

                    GameObject newItem = Instantiate(list[index], randomPoint, Quaternion.identity);
                    lastItemSpawned = newItem;

                    // Consider if you want to destroy the previous item after a certain time
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

