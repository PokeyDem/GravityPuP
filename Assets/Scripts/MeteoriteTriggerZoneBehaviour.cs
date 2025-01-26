using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteoriteTriggerZoneBehaviour : MonoBehaviour{

    [SerializeField] private GameObject _meteorite; 
    [SerializeField] private float _delayInSpawn; //How many seconds must elapse before the next ball spawns
    [SerializeField] private float _eventDuration;

    [SerializeField, Range(0,100), Tooltip("0% - spawns anywhere on screen, 100% - spawns above player")]
    private float _chanceToSpawnOnPlayer;

    [SerializeField] private float _rotation;
    
    private Camera cam;
    private bool _isTriggered;
    private bool _isRunning;
    
    private void Start(){
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (!_isTriggered && other.gameObject.CompareTag("Player")){
            _isTriggered = true;
            StartCoroutine(TimeToSpawn());
        }
    }

    private Vector2 GetRandomPointOnScreen(){
        
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        
        
        float playerPos = (Math.Abs(bottomLeft.x) + Math.Abs(topRight.x)) / 2;
        float offset = playerPos * _chanceToSpawnOnPlayer / 100;
        float randomX = Random.Range(bottomLeft.x + offset, topRight.x - offset);
        float spawnHeight = topRight.y + 2;

        GameObject _player = GameObject.FindWithTag("Player");
        
        offset = Math.Abs(_player.transform.position.x - bottomLeft.x) * ((100 - _chanceToSpawnOnPlayer)/100);
        randomX = Random.Range(_player.transform.position.x - offset, _player.transform.position.x + offset);
        
        return new Vector2(randomX, spawnHeight);
    }
    
    private IEnumerator TimeToSpawn(){
        _isRunning = true;
        StartCoroutine(EventTimer());
        
        while (_isRunning){
            yield return new WaitForSeconds(_delayInSpawn);
            Vector2 point = GetRandomPointOnScreen();
            Quaternion rotation = gameObject.transform.rotation;
            rotation.z = _rotation;
            Instantiate(_meteorite, point, new Quaternion());
        }
    }

    private IEnumerator EventTimer(){
        yield return new WaitForSeconds(_eventDuration);
        _isRunning = false;
    }
    
}
