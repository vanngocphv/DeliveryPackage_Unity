using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPoint : MonoBehaviour
{
    public static RewardPoint Instance { get; private set; }
    public event EventHandler OnDeliverySuccess;
    private int countPoint;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        
    }

    private void FixedUpdate() {
        if (countPoint >= GameManager.Instance.GetPackageNeeded()){
            //Fire a event to Game Manager
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (GameManager.Instance.IsGamePlaying()){
            countPoint++;
            other.transform.position = Vector3.zero;
            other.transform.rotation = new Quaternion(0,0,0,0);
            other.gameObject.SetActive(false);

            OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
        }

        if (countPoint == GameManager.Instance.GetPackageNeeded())
            GameManager.Instance.ChangeIsGameWin(true);
    }


    public void ResetCountPoint(){
        countPoint = 0;
    }
    public int GetCountPoint(){
        return countPoint;
    }
    
}
