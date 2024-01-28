using UnityEngine;

public class HeadDetector : MonoBehaviour
{
    [SerializeField]
    private PlayerStateMachine _player;

    public float Force { set; get; } = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _player.OnItemTrigger(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player") && Force > 0f)
        {
            other.GetComponentInChildren<PlayerStateMachine>().StunAndThrow(other.transform.position - transform.position, Force);
        }
        _player.ResetHead();
    }
}