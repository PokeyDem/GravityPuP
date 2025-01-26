using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstCollectibleDisplayer : MonoBehaviour{
    [SerializeField] private TMP_Text text;

    void Start()
    {
        text.text = "0";
    }

    void Update(){
        text.text = ScoreManager.Instance.GetFirstCollectibleCounter().ToString();
    }
}
