using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour{
    
    [SerializeField] private GameObject _mainView;

    private void Awake(){
        _mainView.SetActive(true);
    } 
    
    public void ExitClicked(){
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void LoadLevel(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
