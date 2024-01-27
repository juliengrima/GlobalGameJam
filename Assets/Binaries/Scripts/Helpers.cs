using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
