using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeteoriteBehaviour : MonoBehaviour{
    
    [SerializeField] private GameObject _groundMark;
    [SerializeField] private float _delayAfterMark; //Delay between the mark of the point of impact and appearance of the ball;
    
    private bool _isExploding;
    private GameObject _currentMark;
    private Animator _animator;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    private void Start(){
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezePosition;

        _sr = GetComponent<SpriteRenderer>();
        _sr.enabled = false;
        
        _animator = GetComponent<Animator>();
        _animator.SetBool("isFlying", true);
        
        SetMarkToTheGround();

        StartCoroutine(DelayBetweenMarkAndProjectileFall());
    }
    
    private void SetMarkToTheGround(){
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -Vector2.up, Mathf.Infinity, layerMask);
        if (hit.collider != null){
            _currentMark = Instantiate(_groundMark, new Vector3(hit.point.x, hit.point.y + 0.1f, 0), gameObject.transform.rotation);
        }

        var vector3 = transform.position;
        vector3.x = vector3.x + 4.25f;
        transform.position = vector3;
    }
    
    private IEnumerator DelayBetweenMarkAndProjectileFall(){
        yield return new WaitForSeconds(_delayAfterMark);
        _sr.enabled = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rb.velocity = new Vector2(-3.7f,0);
    }
    private void OnCollisionEnter2D(Collision2D other){
        
        if (other.gameObject.CompareTag("Ground")){
            _animator.SetBool("isFlying", false);
            _animator.SetBool("hasCollided", true); //To change
            _rb.bodyType = RigidbodyType2D.Static;
            _isExploding = true;
            Destroy(_currentMark);
        }

        if (other.gameObject.CompareTag("Player")){
            Destroy(_currentMark);
            Destroy();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ScoreManager.Instance.ResetScore();
        }
    }
    private void Exploded(){
        _isExploding = false;
    }

    private void Destroy(){ //triggers by action event at last frame of explosion animation
        Destroy(gameObject);
    }
}
