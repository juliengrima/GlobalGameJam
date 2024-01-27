using UnityEngine;

public class HeadDetector : MonoBehaviour
{
    [SerializeField]
    private PlayerStateMachine _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _player.OnItemTrigger(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("hitting a player");
        }
    }
}