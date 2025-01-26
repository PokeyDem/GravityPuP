using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondCollectibleDisplayer : MonoBehaviour{
    
    [SerializeField] private TMP_Text text;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;
    private bool _isVisible;
    

    void Start(){
        _canvasGroup.alpha = 0;
        text.text = "0";
    }

    void Update(){
        text.text = ScoreManager.Instance.GetSecondCollectibleCounter().ToString();
        
        if (text.text != "0")
            FadeIn();
    }

    private void FadeIn(){
        if (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, 1, Time.deltaTime / _fadeDuration);
        }
    }

}
