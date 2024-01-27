using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class EntityMove : MonoBehaviour
{
    #region Champs
    [SerializeField] private PlayerInfo _info;
    [Header("Character_Components")]
    [SerializeField] CharacterController _controller;
    [SerializeField] PlayerStateMachine _playerStateMachine;
    [SerializeField] Grounded _grounded;
    //[SerializeField] HealthCount _health;
    //[SerializeField] EnduanceWheel _stamina;
    [Header("Audio_Component")]
    [SerializeField] AudioSource _source;
    [Header("Fieds")]
    [SerializeField] float _jumpHeight;
    [SerializeField] float _rotation;
    [SerializeField] float _smoothTime;
    //[SerializeField] int _jumpDamage;
    [SerializeField, Range(0, -11)] float _gravity;
    //Privates Components
    Vector3 playerVelocity;
    Vector3 _direction;
    //Privates Fields
    bool _IsJumping;
    bool groundedPlayer;
    float _currentVelocity;

    private Vector2 _targetRotation;

    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _controller = transform.parent.GetComponentInChildren<CharacterController>();

        _gravity = -9.81f;
        _jumpHeight = 1f;
        //_jumpDamage = 1;
    }

    void Update()
    {
        //IsGrounded();
        Moving(_direction);
        //Fall();
    }
    #endregion
    #region Methods
    // Moving method
    public void Moving(Vector2 moving)
    {
        // Jump stuff, enable back if relevant
        /*
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (_IsJumping && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
        }

        playerVelocity.y += _gravity * Time.deltaTime;
        */

        if (moving.magnitude != 0f)
        {
            var forward = moving.y;

            float step = _info.RotationSpeed * Time.deltaTime;

            _controller.transform.Rotate(0f, moving.x * step, 0f);
            _controller.Move(_controller.transform.forward * _info.LinearSpeed * Time.deltaTime * forward);
        }
    }

    public void Jump(InputActionReference _jump)
    {
        _IsJumping = _jump.action.WasPerformedThisFrame();
    }
    #endregion
    #region Coroutines
    #endregion 
}