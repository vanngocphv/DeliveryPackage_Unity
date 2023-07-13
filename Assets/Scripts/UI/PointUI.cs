using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentPoint;
    [SerializeField] private TextMeshProUGUI maxPointNeeded;


    private void Awake(){
        RewardPoint.Instance.ResetCountPoint();
        currentPoint.text = RewardPoint.Instance.GetCountPoint().ToString();
    }

    private void Start(){
        RewardPoint.Instance.OnDeliverySuccess += OnPointUp;
        maxPointNeeded.text = GameManager.Instance.GetPackageNeeded().ToString();
    }

    private void OnPointUp(object sender, System.EventArgs e){
        currentPoint.text = RewardPoint.Instance.GetCountPoint().ToString();
    }
}
