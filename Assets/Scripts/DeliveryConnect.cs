using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryConnect : MonoBehaviour
{
    public event EventHandler<OnRepairConveyorArgs> OnRepairConveyor;
    public class OnRepairConveyorArgs: EventArgs{
        public bool isCompleteRepair;
    }
    private float maxTimeRepair;
    private float countDownTime;
    private Transform deliveryPoint;

    private bool isAvailableForUsing = true;

    private void Start(){
        maxTimeRepair = GameManager.Instance.GetMaxTimeRepair();
    }

    private void FixedUpdate(){
        ClearStatusAfter();
    }
    private void OnTriggerEnter(Collider other) {
        if (deliveryPoint != null){
            if (other.gameObject.TryGetComponent(out IF_DeliveryContent deliveryContent))
            {
                //Only send if receive point same with the connect point and the receive point not null
                if (deliveryContent.GetReceivePoint() == deliveryPoint && deliveryContent.GetReceivePoint() != null){
                    deliveryContent.TransformPositionToNextPoint(deliveryPoint);
                }
                //Check if the delivery content has receive point == null => send this out
                else if (deliveryContent.GetReceivePoint() == null){
                    deliveryContent.TransformPositionToNextPoint(deliveryPoint);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        //the collider will active effect if it has
        if (other.gameObject.TryGetComponent(out IF_DeliveryContent deliveryContent)){
            if (deliveryContent.GetReceivePoint() == null && deliveryPoint != null){
                deliveryContent.SetReceivePoint(deliveryPoint, null);
                deliveryContent.ActiveSpecialEffect();
            }
        }
    }

    public void SetDeliveryPoint(Transform transform){
        deliveryPoint = transform;
    }

    public void ClearDeliveryPoint(){
        deliveryPoint = null;
    }
    public Transform GetDeliveryPoint(){
        return deliveryPoint;
    }

    public bool IsAvailableForUsing(){
        return isAvailableForUsing;
    }
    public void ChangeStatus(bool _status){
        isAvailableForUsing = _status;
        if (!_status){
            countDownTime = maxTimeRepair;
        }
        //fire event with is complete = false
        OnRepairConveyor?.Invoke(this, new OnRepairConveyorArgs {
                isCompleteRepair = _status
            });
    }

    private void ClearStatusAfter(){
        //Status will clear after Count Down Time
        if (isAvailableForUsing == false){
            countDownTime -= Time.deltaTime;
            if (countDownTime < 0f){
                ChangeStatus(true);
            }
        }
    }
    
}
