using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
//using UnityEngine.Rendering.HighDefinition;

public class PlayerStateMachine : MonoBehaviour
{
    #region Champs
    [Header("Player_Controllers")]
    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _eat;
    [SerializeField] InputActionReference _jump;
    //[SerializeField] InputActionReference _crouch;
    //[SerializeField] InputActionReference _look;
    [Header("Player_Camera_component")]
    //[SerializeField] AttachedCameras _cameras;
    [Header("Player_Actions_Components")]
    [SerializeField] EntityMove _entityMove;
    [SerializeField] EntityFire _entityFire; //EAT
    //[SerializeField] PlayGame _playGame;
    //[SerializeField] HealthCount _health;
    //[SerializeField] EntityJump _entityJump;
    //[SerializeField] EntityCrouch _entityCrouch;
    //[SerializeField] EntityLook _entityLook;
    //[SerializeField] CoroutinesStates _coroutines;
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
    //Private Components
    Coroutine _fallCoroutine;
    Coroutine _startCoroutine;
    PlayerState _currentState = PlayerState.START;
    //Public informations
    public float FallWait { get => _fallWait; }
    public float DestroyDisableDuration { get => _destroyDisableDuration; }
    public float LoadingSceneDuration { get => _loadingSceneDuration; }
    public bool Death { get => _death; set => _death = value; }
    public InputActionReference Move { get => _move; }
    public InputActionReference Jump { get => _jump; }
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
    #region Enumerator
    // Cerveau du squelette
    public enum PlayerState
    {
        IDLE,
        WALK,
        EAT,
        JUMP,
        CROUCH,
        FALL,
        DEATH,
        START
    }
    #endregion
    #region BeforeStart
    private void Reset()
    {
        // Call components when LevelDesigner take it to the Hierarchy
        _entityMove = transform.parent.GetComponentInChildren<EntityMove>();
        //_entityJump = transform.parent.GetComponentInChildren<EntityJump>();
        //_entityCrouch = transform.parent.GetComponentInChildren<EntityCrouch>();
        //_entityLook = transform.parent.GetComponentInChildren<EntityLook>();
        _Grounded = transform.parent.GetComponentInChildren<Grounded>();
        //_interactions = transform.parent.GetComponentInChildren<Interactions>();
        _animator = transform.parent.GetComponentInChildren<Animator>();
        //_coroutines = transform.parent.GetComponentInChildren<CoroutinesStates>();

        //All informations datas at start
        _fallWait = 3f;
        _destroyDisableDuration = 5f;
        _loadingSceneDuration = 5f;
        _death = false;
    }
    #endregion
    #region Unity LifeCycle
    // Update is called once per frame
    void Update()
    {
        OnStateUpdate();
    }
    #endregion
    #region Methods
    #endregion
    #region States
    // Colonne Vertébrale
    void OnStateEnter()
    {
        switch (_currentState)
        {
            case PlayerState.IDLE:
                //StopAllCoroutines();
                //_animator.SetFloat("ISMOVING", 0);
                //_animator.SetFloat("Y", 0);
                //_animator.SetBool("ISFALLING", false);
                _dir = new Vector3(0, 0, 0);
                break;
            case PlayerState.WALK:
                break;
            case PlayerState.EAT:
                //_animator.SetBool("ISEATING", true);\
                break;
            case PlayerState.JUMP:
                //_animator.SetTrigger("JUMP");
                break;
            case PlayerState.CROUCH:
                break;
            case PlayerState.FALL:
                break;
            case PlayerState.DEATH:
                _death = true;
                //_cameras.VirtualsCameras(_death);
                StopAllCoroutines();
                //_animator.SetFloat("ISMOVING", 0);
                //_animator.SetFloat("Y", 0);
                //_animator.SetBool("ISFALLING", false);
                break;
            case PlayerState.START:
                //_cameras.VirtualsCameras(true);
                break;
            default:
                break;
        }
    }
    void OnStateUpdate()
    {
        //_entityLook.Look(this);
        _dir = _move.action.ReadValue<Vector2>();

        switch (_currentState)
        {
            case PlayerState.IDLE: //Base statement
                //Debug.Log("Is Grounded");
                if (_dir.magnitude > 0)
                {
                    //Debug.Log("Is Moving");
                    TransitionToState(PlayerState.WALK);
                }
                else if (_jump.action.WasPerformedThisFrame())
                {
                    //Debug.Log("Is Jumping");
                    TransitionToState(PlayerState.JUMP);
                }
                //else if (_crouch.action.WasPerformedThisFrame())
                //{
                //    //Debug.Log("Is crouching");
                //    TransitionToState(PlayerState.CROUCH);
                //}
                
                //Fire will come
                _entityMove.Moving(_dir);
                break;
            case PlayerState.WALK: // State Start to move and make interactions
                //_animator.SetFloat("ISMOVING", _dir.x);
                //_animator.SetFloat("Y", _dir.y);
                if (_dir.magnitude <= 0f)
                {
                    TransitionToState(PlayerState.IDLE);
                }
                if (_eat.action.WasPerformedThisFrame())
                {
                    TransitionToState(PlayerState.EAT);
                }
                else if (_jump.action.WasPerformedThisFrame())
                {
                    TransitionToState(PlayerState.JUMP);
                }
                //else if (_crouch.action.WasPerformedThisFrame())
                //{
                //    TransitionToState(PlayerState.CROUCH);
                //}

                //_entityMove.Moving(_dir);
                //_interactions.Interations();
                //Fire will come
                break;
            case PlayerState.EAT:

                if (_dir.magnitude <= 0f)
                {
                    TransitionToState(PlayerState.IDLE);
                }

                //_entityMove.Run(_run);
                break;
            case PlayerState.JUMP:

                if (_move.action.WasPerformedThisFrame())
                {
                    TransitionToState(PlayerState.WALK);
                }
                //else if (_jump.action.WasPerformedThisFrame())
                //{
                //    TransitionToState(PlayerState.JUMP);
                //}
                //else if (_crouch.action.WasPerformedThisFrame())
                //{
                //    TransitionToState(PlayerState.CROUCH);
                //}
                else
                {
                    TransitionToState(PlayerState.IDLE);
                }

                //Debug.Log(" Go to Jump() Method");
                _entityMove.Jump(_jump);

                break;
            case PlayerState.CROUCH:

                //if (_crouch.action.WasPerformedThisFrame())
                //{
                //    TransitionToState(PlayerState.IDLE);
                //}
                //Fire Will come

                break;
            case PlayerState.FALL:
                break;
            case PlayerState.DEATH:
                //_coroutines.StartCoroutine(_coroutines.DeathReload(this));
                //_animator.SetTrigger("DEATH");
                break;
            case PlayerState.START:
                //_startCoroutine = _coroutines.StartCoroutine(_coroutines.MyStartCoroutines(this));
                TransitionToState(PlayerState.IDLE);
                break;
            default:
                break;
        }
    }
    void OnStateExit()
    {
        switch (_currentState)
        {
            case PlayerState.IDLE:
                break;
            case PlayerState.WALK:
                break;
            case PlayerState.EAT:
                //_animator.SetBool("ISEATING", false);
                break;
            case PlayerState.JUMP:
                break;
            case PlayerState.CROUCH:
                //_animator.SetBool("CROUCH", false);
                break;
            case PlayerState.FALL:
                //_animator.SetBool("ISFALLING", false);
                break;
            case PlayerState.DEATH:
                //_animator.SetTrigger("DEATH");
                break;
            case PlayerState.START:
                //_animator.SetBool("START", false);
                //_cameras.VirtualsCameras(false);
                break;
            default:
                break;
        }
    }

    public void TransitionToState(PlayerState nextState)
    {
        OnStateExit();
        _currentState = nextState;
        OnStateEnter();
    }
    #endregion
    #region Coroutines
    #endregion

}
