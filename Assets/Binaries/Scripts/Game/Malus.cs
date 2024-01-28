using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malus : Item
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ColliderReceptor>().InvokeItemTrigger(other.gameObject);
            Destroy(gameObject);
        }
    }
}
