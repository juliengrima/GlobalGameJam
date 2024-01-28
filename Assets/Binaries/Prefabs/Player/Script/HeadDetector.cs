using UnityEngine;

public class HeadDetector : MonoBehaviour
{
    [SerializeField]
    private PlayerStateMachine _player;

    private void OnTriggerEnter(Collider other)
    {
        _player.StunAndThrow(transform.position - other.transform.position);
        if (other.CompareTag("Item"))
        {
            _player.OnItemTrigger(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<PlayerStateMachine>().StunAndThrow(other.transform.position - transform.position);
        }
        _player.ResetHead();
    }
}