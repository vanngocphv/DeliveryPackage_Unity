using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPauseUI : MonoBehaviour
{
    
    [SerializeField] private Button pauseButton;

    private void Awake(){
        pauseButton.onClick.AddListener(OnClickPauseButtonInMenu);
    }

    private void OnClickPauseButtonInMenu(){
        PauseGameUI.Instance.Show();
    }
}
