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
    //[SerializeField] int _jumpDamage;
    [SerializeField, Range(0, -11)] float _gravity;
    //Privates Components
    Vector3 playerVelocity;
    Vector3 _direction;
    Vector2 _oldDirection;
    Coroutine _walkCoroutine;
    //Privates Fields
    bool _isRunning;
    bool _IsJumping;
    bool groundedPlayer;

    public float Gravity { get => _gravity; set => _gravity = value; }
    public AudioSource Source { get => _source; set => _source = value; }
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

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * _speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
        }

        playerVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }


    public void Run(InputActionReference run)
    {
        _isRunning = run.action.IsPressed();
    }
    public void Jump(InputActionReference _jump)
    {
        _IsJumping = _jump.action.WasPerformedThisFrame();
    }

    // 
    //private void Fall()
    //{
    //    var fall = playerVelocity.y += _controller.velocity.y * -3.0f * _gravity;

    //    if (fall > 1f && groundedPlayer)
    //    {
    //        int jumpDamage = Convert.ToInt32(fall) * _jumpDamage;
    //        _health.TakeDamage(jumpDamage);
    //    }
    //}

    //void IsGrounded()
    //{
    //    //groundedPlayer = _controller.isGrounded;
    //    groundedPlayer = _grounded.IsGrounded;
    //    if (groundedPlayer)
    //    {
    //        playerVelocity.y = 0f;
    //    }
    //    // Update gravity
    //    playerVelocity.y += _gravity * Time.deltaTime;
    //}
    #endregion
    #region Coroutines
    #endregion 
}