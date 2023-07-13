using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameUI : MonoBehaviour
{
    public static PauseGameUI Instance { get; private set; }
    [SerializeField] private Button continueButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;

    private void Awake(){
        Instance = this;

        continueButton.onClick.AddListener(OnClickContinueButton);
        retryButton.onClick.AddListener(OnClickRetryButton);
        menuButton.onClick.AddListener(OnClickMenuButton);

        HideGameObject();
    }


    private void HideGameObject(){
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void Hide(){
        HideGameObject();
    }
    private void ShowGameObject(){
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
    public void Show(){
        ShowGameObject();
    }



    private void OnClickContinueButton(){
        HideGameObject();
    }

    private void OnClickRetryButton(){
        LoadingStaticClass.SetTargetScene(LoadingStaticClass.Scene.GameScene);
    }

    private void OnClickMenuButton(){
        LoadingStaticClass.SetTargetScene(LoadingStaticClass.Scene.MenuScene);

    }
}
