using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
//using UnityEngine.Rendering.HighDefinition;

public class PlayerStateMachine : MonoBehaviour
{
    #region Champs
    [SerializeField] private PlayerInfo _info;
    [SerializeField] private GameObject _mainParent;
    [Header("Player_Actions_Components")]
    [SerializeField] EntityMove _entityMove;
    [Header("Player_Animations")]
    [SerializeField] Animator _animator;
    //[SerializeField] Animation _animation;
    [Header("Player_Audios")]
    [SerializeField] AudioSource _source;
    //Private Fields
    bool _death;
    Vector2 _dir;
    private bool _isInit;

    private int _mashCounter;

    private int _itemEattenCount;
    #endregion

    public void OnItemTrigger(GameObject go)
    {
        Eat();
    }

    public void Eat()
    {
        _itemEattenCount++;
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        _dir = value.ReadValue<Vector2>().normalized;
    }

    public void OnMash(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            _mashCounter++;
        }
    }

    public void OnHit(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            var hit01 = _mashCounter / (float)_info.MaxHitCount;
            var distance = _info.DistanceCurve.Evaluate(hit01) * _info.MaxDistance;

            Debug.Log($"Distance reached: {distance}");
            // TODO: Throw head here! Use distance to know how far you go

            _mashCounter = 0;
        }
    }

    private void Update()
    {
        _entityMove.Moving(_dir);
    }

    private void LateUpdate()
    {
        if (!_isInit)
        {
            _isInit = true;
            PlayerManager.Instance.Register(_mainParent);
        }
    }
}
