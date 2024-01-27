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
    [SerializeField] EntityFire _entityFire; //EAT
    [Header("Player_interactions_Components")]
    [SerializeField] Grounded _Grounded;
    //[SerializeField] Interaction _interaction;
    [Header("Player_Animations")]
    [SerializeField] Animator _animator;
    //[SerializeField] Animation _animation;
    [Header("Player_Audios")]
    [SerializeField] AudioSource _source;
    [Header("Informations_fields")]
    [SerializeField] float _fallWait;
    [SerializeField] float _destroyDisableDuration;
    [SerializeField] float _loadingSceneDuration;
    [Header("Events_Components")]
    [SerializeField] UnityEvent _explosion1;
    [SerializeField] UnityEvent _explosion2;
    [SerializeField] UnityEvent _explosion3;
    //Private Fields
    bool _death;
    Vector2 _dir;
    private bool _isInit;

    private int _mashCounter;
    //Private Components
    Coroutine _fallCoroutine;
    Coroutine _startCoroutine;
    //Public informations
    public float FallWait { get => _fallWait; }
    public float DestroyDisableDuration { get => _destroyDisableDuration; }
    public float LoadingSceneDuration { get => _loadingSceneDuration; }
    public bool Death { get => _death; set => _death = value; }
    //public InputActionReference Look { get => _look; set => _look = value; }
    //public PlayGame PlayGame { get => _playGame; set => _playGame = value; }
    public Vector2 Dir { get => _dir; set => _dir = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
    //public HealthCount Health { get => _health; set => _health = value; }
    public UnityEvent Explosion1 { get => _explosion1; set => _explosion1 = value; }
    public UnityEvent Explosion2 { get => _explosion2; set => _explosion2 = value; }
    public UnityEvent Explosion3 { get => _explosion3; set => _explosion3 = value; }
    public AudioSource Source { get => _source; set => _source = value; }

    //public Animation Animation { get => _animation; set => _animation = value; }

    //public InputActionReference Pause { get => _pause; }

    #endregion
    #region BeforeStart

    private void Reset()
    {
        // Call components when LevelDesigner take it to the Hierarchy
        _entityMove = transform.parent.GetComponentInChildren<EntityMove>();
        _Grounded = transform.parent.GetComponentInChildren<Grounded>();
        _animator = transform.parent.GetComponentInChildren<Animator>();

        //All informations datas at start
        _fallWait = 3f;
        _destroyDisableDuration = 5f;
        _loadingSceneDuration = 5f;
        _death = false;
    }
    #endregion

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
