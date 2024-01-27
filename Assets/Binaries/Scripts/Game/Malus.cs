using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malus : Item
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ColliderReceptor>().InvokeItemTrigger(other.gameObject);
            Destroy(gameObject);
        }
    }
}
