using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorConnect : MonoBehaviour
{
    private void OnTriggerExit(Collider other) {
        Debug.Log("The trigger exit");
        if (other.gameObject.TryGetComponent(out IF_DeliveryContent deliveryContent)){
            if (deliveryContent.GetReceivePoint() == null){
                deliveryContent.ActiveSpecialEffect();
            }
        }
    }
}
