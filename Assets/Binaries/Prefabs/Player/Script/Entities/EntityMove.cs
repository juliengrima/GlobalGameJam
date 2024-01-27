using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class EntityMove : MonoBehaviour
{
    #region Champs
    [Header("Character_Components")]
    [SerializeField] CharacterController _controller;
    [SerializeField] PlayerStateMachine _playerStateMachine;
    [SerializeField] Grounded _grounded;
    //[SerializeField] HealthCount _health;
    //[SerializeField] EnduanceWheel _stamina;
    [Header("Audio_Component")]
    [SerializeField] AudioSource _source;
    [Header("coroutines_Component")]
    [SerializeField] CoroutinesList _coroutines;
    [Header("Fieds")]
    [SerializeField] float _speed;
    [SerializeField] float _jumpHeight;
    [SerializeField] float _rotation;
    [SerializeField, Range(1, 5)] float _rotationSpeed;
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

   

    public AudioSource Source { get => _source; set => _source = value; }
    public CharacterController Controller { get => _controller; set => _controller = value; }
    public Vector3 Direction { get => _direction; set => _direction = value; }
    public float Rotation { get => _rotation; set => _rotation = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
    public float SmoothTime { get => _smoothTime; set => _smoothTime = value; }
    public float Gravity { get => _gravity; set => _gravity = value; }
    public float CurrentVelocity { get => _currentVelocity; set => _currentVelocity = value; }
    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _controller = transform.parent.GetComponentInChildren<CharacterController>();

        _gravity = -9.81f;
        _speed = 1.5f;
        _jumpHeight = 1f;
        //_jumpDamage = 1;
    }

    void Update()
    {
        //IsGrounded();
        Moving(_direction);
        Jump(_playerStateMachine.Jump);
        //Fall();
    }
    #endregion
    #region Methods
    // Moving method
    public void Moving(Vector2 moving)
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        _direction = moving;

        if (_direction.sqrMagnitude == 0) return;
        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(_controller.transform.eulerAngles.y, targetAngle, ref _currentVelocity, _smoothTime);

        //_playerStateMachine.Move

        _controller.transform.rotation = Quaternion.Euler(0, angle, 0);

        Vector3 move = new Vector3(_direction.x, 0, _direction.y);
        _controller.Move(move * Time.deltaTime * _speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (_IsJumping && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
        }

        playerVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump(InputActionReference _jump)
    {
        _IsJumping = _jump.action.WasPerformedThisFrame();
    }
    #endregion
    #region Coroutines
    #endregion 
}