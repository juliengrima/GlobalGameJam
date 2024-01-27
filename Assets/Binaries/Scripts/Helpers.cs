using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static Vector3 GetRandomPointInBounds(Bounds bounds, Vector3 previousSpawnPosition, float minDistance,float bufferDistance)
    {
        Vector3 randomPoint = Vector3.zero;
        do
        {
            float randomX = Random.Range(bounds.min.x + bufferDistance, bounds.max.x - bufferDistance);
            float randomZ = Random.Range(bounds.min.z + bufferDistance, bounds.max.z - bufferDistance);

            randomPoint = new Vector3(randomX, 0, randomZ);

        } while (Vector3.Distance(randomPoint, previousSpawnPosition) < minDistance);

        return randomPoint;
    }

    public static int GetRandomInt(int min, int max)
    {
        int rand = UnityEngine.Random.Range(0, max);
        return rand;
    }
}
