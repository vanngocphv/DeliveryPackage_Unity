using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private Image timeImage;
    [SerializeField] private float maxTimeNeeded;
    private float currentTimeLeft;

    private void Start(){
        maxTimeNeeded = GameManager.Instance.GetMaxTimeCanPlay();
        currentTimeLeft = maxTimeNeeded;
    }

    private void Update(){
        if (GameManager.Instance.IsGamePlaying()){
            currentTimeLeft -= Time.deltaTime;
            if (currentTimeLeft < 0){
                currentTimeLeft = 0;
                GameManager.Instance.ChangeIsGameOver(true);
            }
            timeImage.fillAmount = 1 - currentTimeLeft/maxTimeNeeded;
        }
    }


}
