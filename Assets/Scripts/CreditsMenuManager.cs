using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainView;

    private void Awake(){
        _mainView.SetActive(true);
    } 
    

    public void LoadLevel(string sceneName){
        Scene scene = SceneManager.CreateScene(sceneName);
    }
}
