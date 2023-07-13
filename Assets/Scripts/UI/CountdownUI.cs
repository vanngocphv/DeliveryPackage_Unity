using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUI : MonoBehaviour
{
    private const string CONST_IS_COUNTDOWN = "isCountDown";
    private const string CONST_START = "START!";
    [SerializeField] private TextMeshProUGUI countDownText;
    private float countTime;
    private int previousCountTime;
    private Animator animator;

    private void Start(){
        countTime = GameManager.Instance.GetMaxTimeLoadingMain();
        animator = GetComponent<Animator>();
        GameManager.Instance.OnGameCountDown += OnGameCountDownStart;
        HideGameObject();
    }

    private void Update(){
        if (GameManager.Instance.isGameLoadingMain()){
            int countTimeToInt = Mathf.CeilToInt(countTime);
            if (previousCountTime != countTimeToInt){
                countDownText.text = countTimeToInt.ToString();
                animator.SetTrigger(CONST_IS_COUNTDOWN);
                //Set previous time = current time text
                previousCountTime = countTimeToInt;
            }

            if (countTime < 0f){
                countDownText.text = CONST_START;
                if (countTime < -1f){
                    GameManager.Instance.ChangeIsGamePlaying(true);
                    HideGameObject();
                }
            }

            countTime -= Time.deltaTime;
        }
    }

    private void OnGameCountDownStart(object sender, System.EventArgs e){
        ShowGameObject();
    }

    private void ShowGameObject(){
        this.gameObject.SetActive(true);
    }

    private void HideGameObject(){
        this.gameObject.SetActive(false);
    }
}
