using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _speed = 5;
    [SerializeField] private GameObject _flame;
    private SpriteRenderer _flameRenderer;
    private Rigidbody2D _rb;
    private float _xInput;
    private bool _canJump;
    private bool _canDoubleJump;
    private bool _isGrounded;
    private JetPack _jetPack;
    private SpriteRenderer _sr;
    private bool isKnockedBack = false;
    private float knockbackDuration = 0.2f;
    private float knockbackTimer;
    private float _maxSpeed = 5f;
    private PlayerAnimations _playerAnimations;
    
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _jetPack = GetComponent<JetPack>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _flameRenderer = _flame.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _xInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _canJump = true;
        }
        else if (Input.GetButtonDown("Jump") && _jetPack.InAir)
        {
            _jetPack.Thrusting = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            _jetPack.Thrusting = false;
            _flameRenderer.enabled = false;

        }
    }

    private void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            _playerAnimations.Run(_xInput);
            _playerAnimations.inAir(_rb.velocity.y);
            _rb.velocity =new Vector2(_xInput * _speed,_rb.velocity.y);
            
            if (_canJump)
            {
                _canJump = false;
                _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
                _jetPack.InAir = true;
                AudioManager.Instance.PlaySFX("Jump");
            }
        }
        else
        {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
            }
        }

        Flip();
    }

    public void ApplyKnockback(Vector2 force, float duration)
    {
        isKnockedBack = true;
        knockbackTimer = duration;
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        if (_xInput > 0 && _sr.flipX)
        {
            _sr.flipX = false;
            _flame.transform.localPosition = new Vector3(-0.24f, 0.05f, 0);
            _flame.transform.Rotate(0, 0, -40); 
        }
        else if (_xInput < 0 && !_sr.flipX)
        {
            _sr.flipX = true;
            _flame.transform.localPosition = new Vector3(0.24f, 0.05f, 0);
            _flame.transform.Rotate(0, 0, 40); 
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
        _jetPack.InAir = false;
        _playerAnimations.Land();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
        _playerAnimations.TakeOff();
    }
}