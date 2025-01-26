using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour{

    [SerializeField, Range(1, 2)] private int type; 
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            if (type == 1)
                ScoreManager.Instance.CollectFirst();
            else
                ScoreManager.Instance.CollectSecond();
            
            Destroy(gameObject);
        }
    }
}
