using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public UnityEvent OnDestroyEvt { get; } = new();

    private void OnDestroy()
    {
        OnDestroyEvt.Invoke();
    }
}
