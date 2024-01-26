using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerStateMachine : MonoBehaviour
{
    #region Champs
    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _run;
    [SerializeField] InputActionReference _jump;
    [SerializeField] InputActionReference _crouch;
    [SerializeField] InputActionReference _look;

    [Header("Player_Camera_component")]
    //[SerializeField] AttachedCameras _cameras;
    [Header("Player_Actions_Components")]
    //[SerializeField] EntityMove _entityMove;
    //[SerializeField] PlayGame _playGame;
    //[SerializeField] HealthCount _health;
    //[SerializeField] EntityJump _entityJump;
    //[SerializeField] EntityCrouch _entityCrouch;
    //[SerializeField] EntityLook _entityLook;

    [Header("Player_interactions_Components")]
    //[SerializeField] Grounded _Grounded;
    //[SerializeField] Interaction _interaction;
    [Header("Player_Animations")]
    [SerializeField] Animator _animator;
    [SerializeField] Animation _animation;
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

    PlayerState _currentState;
    #endregion
    #region Enumerator
    public enum PlayerState
    {
        State1,
        State2,
        State3,
        State4,
        State5,
        State6,
        State7
    }
    #endregion
    #region Default Informations
    void Reset()
    {
        
    }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    
    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Methods
    void FixedUpdate()
    {
        
    }
    void LateUpdate()
    {
        
    }
    #endregion
    #region StatesMachine
    void OnStateEnter()
    {
        switch (_currentState)
        {
            case PlayerState.State1:
               
                break;
            case PlayerState.State2:
                break;
            case PlayerState.State3:
                break;
            case PlayerState.State4:
               
                break;
            case PlayerState.State5:
               
                break;
            case PlayerState.State6:
               
                break;
            case PlayerState.State7:
                
                break;
            default:
                break;
        }
    }
    void OnStateUpdate()
    {
        switch (_currentState)
        {
            case PlayerState.State1: //Base statement 
                break;
            case PlayerState.State2: // State Start to move and make interactions
                break;
            case PlayerState.State3:
                break;
            case PlayerState.State4:
                break;
            case PlayerState.State6:    
                break;
            case PlayerState.State7:

                break;
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (_currentState)
        {
            case PlayerState.State1:
              
                break;
            case PlayerState.State2:
               
                break;
            case PlayerState.State3:
                
                break;
            case PlayerState.State4:
                break;
            case PlayerState.State5:
                
                break;
            case PlayerState.State6:
               
                break;
            case PlayerState.State7:

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
	IEnumerator EndCoroutine()
    {
        throw new NotImplementedException();   
    }
    #endregion
}