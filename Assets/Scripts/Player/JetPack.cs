using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class JetPack : MonoBehaviour
{
    [SerializeField] private float _fuel=3f;
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _force = 1f;
    [SerializeField] private float _cooldown=3f;
    private bool _fueled=true;
    private bool _inAir;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    public bool InAir { get; set; }
    public bool Thrusting { get; set; }
    


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        

    }

    private void Update()
    {
        
    }


    private void FixedUpdate()
    {
        //Debug.Log("InAir: "+InAir+" Fuel: "+_fuel+" Thrusting: "+Thrusting);
        if (Thrusting&& _fueled)
        {
            JetPackThrust();
        }

        if (!InAir)
        {
            _fueled = true;
            _fuel = _maxFuel;
        }
        
    }
    


    private void JetPackThrust()
    {
            _fuel -= Time.fixedDeltaTime;
        
            _rigidbody.AddForce(new Vector2(0,_force),ForceMode2D.Impulse);
            // AudioManager.Instance.PlaySFX("Jetpack");
            if (Input.GetKey(KeyCode.Space))
            {
                if (!AudioManager.Instance.sfxSource.isPlaying)
                {
                    AudioManager.Instance.PlaySFX("Jetpack");
                }
            }
            else
            {
                if (!AudioManager.Instance.sfxSource.isPlaying)
                {
                    AudioManager.Instance.PlaySFX("Jetpack");
                }
            }
            
            _fueled = _fuel >= 0;
        
    }
}
