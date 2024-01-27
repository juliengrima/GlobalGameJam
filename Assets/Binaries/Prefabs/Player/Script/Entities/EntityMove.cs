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
    #region Default Informations
    void Reset()
    {
        _controller = transform.parent.GetComponentInChildren<CharacterController>();

        _gravity = -9.81f;
        _speed = 1.5f;
        _jumpHeight = 1f;
        //_jumpDamage = 1;
    }
    #endregion
    #region Unity LifeCycle
    // Update is called once per frame
    void Update()
    {
        Moving(_direction);
        IsGrounded();
    }
    #endregion
    #region Methods
    public void Moving(Vector2 moving)
    {
        // Vector2 moving = PlayerstateMachine Vector2 _dir
        //_oldDirection = moving;
        _direction = moving;
        Vector3 move = new Vector3(_direction.x, 0, _direction.y);
        move = _controller.transform.TransformDirection(move);

        _controller.Move(move * Time.deltaTime * _speed);

        //// Add JumpForce if Jump is pressed
        //if (_IsJumping && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
        //    // Give impulsion and multipli gravity & force to jumpheight
        //}
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump(InputActionReference _jump)
    {
        _IsJumping = _jump.action.WasPerformedThisFrame();
    }

    void IsGrounded()
    {
        //groundedPlayer = _controller.isGrounded;
        groundedPlayer = _grounded.IsGrounded;
        if (groundedPlayer)
        {
            playerVelocity.y = 0f;
        }
        // Update gravity
        playerVelocity.y += _gravity * Time.deltaTime;
    }
    #endregion
    #region Coroutines
    #endregion
}