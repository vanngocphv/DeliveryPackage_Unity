using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailConnect : MonoBehaviour
{
    [SerializeField] private DeliveryConnect mainConveyor;

    private void Start(){

    }

    //Set delivery point for delivery 
    public void SetDeliveryPoint(Transform transform){
        mainConveyor.SetDeliveryPoint(transform);
        
    }

    public void ClearDeliveryPoint(){
        mainConveyor.ClearDeliveryPoint();
    }
    public Transform GetDeliveryPoint(){
        return mainConveyor.GetDeliveryPoint();
    }

    public Transform GetMainConveyor(){
        return mainConveyor.transform;
    }
    
}
