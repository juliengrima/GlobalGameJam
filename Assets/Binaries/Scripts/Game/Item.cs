using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public UnityEvent OnDestroyEvt { get; } = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ColliderReceptor>().InvokeItemTrigger(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnDestroyEvt.Invoke();
    }
}
