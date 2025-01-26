using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class BlackHallBehaviour : MonoBehaviour{
    private GameObject _player;
    private float _maxDist = 4.744f;
    private bool _isPlayerInTrigger;
    private Rigidbody2D _playerRb;
    private float _gravitationalForce = 15f;
    [SerializeField] private float _gravityMultiplier = 0.6f;
    private bool _isAttaching = true;

    private void Awake(){
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRb = _player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            _isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            _isPlayerInTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        
    }

    private void Update(){
        if (_isPlayerInTrigger){
            Vector2 direction = (Vector2)transform.position - _playerRb.position;
        
            float distance = direction.magnitude;
            float normalizedDistance = 1 - (distance / _maxDist);

            // Vector2 newPos = _playerRb.position;
            // newPos.x = Mathf.Lerp(newPos.x, transform.position.x, Time.deltaTime * (normalizedDistance + _gravityMultiplier));
            // newPos.y = Mathf.Lerp(newPos.y, transform.position.y + 1, Time.deltaTime * (normalizedDistance + _gravityMultiplier));
            // Vector2 perpendicularForce = Vector2.Perpendicular(direction).normalized;
            // _playerRb.AddForce(perpendicularForce * 2f);
            // _player.transform.position = newPos;
            //
            //
            // if (distance < 0.7){
            //     string scene = SceneManager.GetActiveScene().name;
            //     SceneManager.LoadScene(scene);
            // }
            //
            // if (direction.x < 0){
            //     Debug.Log("Triggered");
            //     _playerRb.AddForce(Vector2.right * Vector2.up * 100, ForceMode2D.Impulse);
            // }

            //Option 2:
            if (distance > 0)
            {
                Vector2 forceDirection = direction.normalized;
                
                float forceMagnitude = _gravitationalForce / Mathf.Pow(distance, 2) * 1.5f;

                if (forceMagnitude > 4)
                    forceMagnitude = 2;

                Debug.Log(forceDirection * forceMagnitude);
                _playerRb.AddForce(forceDirection * forceMagnitude);

                if (distance < 1.4){
                    Vector2 _dir = _playerRb.velocity.normalized;
                    
                    // if (_dir.y < 0)
                    //     _dir.y = -_dir.y;
                    //
                    // _dir.x += 0.7f;
                    
                    _playerRb.AddForce( _dir * 0.06f, ForceMode2D.Impulse);
                }
                if (distance < 0.6){
                    string scene = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(scene);
                }
            }
        } 
    }
}
