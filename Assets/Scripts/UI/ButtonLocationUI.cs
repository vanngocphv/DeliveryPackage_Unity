using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLocationUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button quitButton;

    private void Awake(){
        playButton.onClick.AddListener(OnClickPlayButton);
        creditButton.onClick.AddListener(OnClickCreditButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
    }


    private void OnClickPlayButton(){
        LoadingStaticClass.SetTargetScene(LoadingStaticClass.Scene.GameScene);
    }

    private void OnClickCreditButton(){
        //Do nothing
    }

    private void OnClickQuitButton(){
        Application.Quit();
    }
}
