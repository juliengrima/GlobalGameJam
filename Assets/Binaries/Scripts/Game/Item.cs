using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Action onEat;

    [SerializeField] private bool isEaten;

    private void Update()
    {
        if (isEaten)
        {
            IsEat();
        }
    }

    public void IsEat()
    {
        isEaten = !isEaten;

        Destroy(this.gameObject);

        Debug.Log("Item was eaten by Player : " /* + GetPlayerID()*/);

        onEat?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isEaten)
        {
            IsEat();
        }
    }
}
