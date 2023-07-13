using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake(){
        menuButton.onClick.AddListener(OnClickMenuButton);
        retryButton.onClick.AddListener(OnClickRetryButton);
    }

    private void Start(){
        HideGameObject();
    }

    private void OnClickMenuButton(){
        LoadingStaticClass.SetTargetScene(LoadingStaticClass.Scene.MenuScene);
    }

    private void OnClickRetryButton(){
        LoadingStaticClass.SetTargetScene(LoadingStaticClass.Scene.GameScene);
    }

    private void HideGameObject(){
        this.gameObject.SetActive(false);
    }

    public void ShowGameObject(){
        this.gameObject.SetActive(true);
        highScoreText.text = RewardPoint.Instance.GetCountPoint().ToString();
    }


}
