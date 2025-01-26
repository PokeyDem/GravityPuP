using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour{

    public static ScoreManager Instance{ get; private set; }

    private int _collected_1 = 0;
    private int _collected_2 = 0;
    private int total_1;
    private int total_2;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start(){
        total_1 = GameObject.FindGameObjectsWithTag("Collectible_1").Length;
        total_2 = GameObject.FindGameObjectsWithTag("Collectible_2").Length;
    }

    public void ResetScore(){
        _collected_1 = 0;
        _collected_2 = 0;
    }

    public void CollectFirst(){
        _collected_1++;
    }

    public void CollectSecond(){
        _collected_2++;
    }
    public int GetFirstCollectibleCounter(){
        return _collected_1;
    }

    public int GetSecondCollectibleCounter(){
        return _collected_2;
    }
}
