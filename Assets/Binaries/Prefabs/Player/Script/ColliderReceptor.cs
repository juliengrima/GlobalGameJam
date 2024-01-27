using UnityEngine;
using UnityEngine.Events;

public class ColliderReceptor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<GameObject> _onItemTrigger = new();

    public void InvokeItemTrigger(GameObject other)
    {
        _onItemTrigger.Invoke(other);
    }
}