using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]private GameObject _flame;
    private Animator _flameAnimator;
    private JetPack _jetPack;
    private SpriteRenderer _jetPackSprite;

    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _flameAnimator = _flame.GetComponent<Animator>();
        _jetPack = GetComponent<JetPack>();
        _jetPackSprite = _flame.GetComponent<SpriteRenderer>();

    }

    public void Run(float xSpeed)
    {
        _animator.SetFloat("xSpeed",Math.Abs(xSpeed));
        ResetTriggers();
    }
    

    public void inAir(float ySpeed)
    {
        switch (ySpeed)
        {
            case < 0.1f and > -0.1f:
                _animator.SetTrigger("isHovering");
                _flameAnimator.SetBool("isThrusting",false);
                break;
            case < -0.1f:
                _animator.SetTrigger("isFalling" );
                DontThrust();
                break;
            case > 0.1f:
                if (_jetPack.Thrusting)
                {
                    _jetPackSprite.enabled = true;
                    _flameAnimator.SetBool("isThrusting",true);
                    
                    
                } 
                _animator.SetTrigger("isJumping");
                
                break;
        }
    }

    public void TakeOff()
    {
        _animator.SetBool("isGrounded",false);
    }

    public void Land()
    {
        ResetTriggers();
        _animator.SetBool("isGrounded",true);
        

    }

    private void ResetTriggers()
    {
        _animator.ResetTrigger("isFalling");
        _animator.ResetTrigger("isJumping");
        _animator.ResetTrigger("isHovering");
        _flameAnimator.SetBool("isThrusting",false);
        
    }

    private void DontThrust()
    {
        _flameAnimator.SetBool("isThrusting",false);
        _jetPackSprite.enabled = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
