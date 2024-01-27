using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool isEaten;

    private void Update()
    {
        if (isEaten)
        {
            isEaten = !isEaten;
            Debug.Log("Item was eaten");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEaten = true;
            Debug.Log("Collision w/ Player" /* + GetPlayerID()*/);
        }
    }
}
