using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour, IF_DeliveryContent
{
    [SerializeField] private PackageVisualUI packageVisualUI;

    [SerializeField] private Transform receivePoint;
    [SerializeField] private string pointName;
    [SerializeField] private GameObject particleEffect;

    private void Awake(){
        hideParticleEffect();
    }
    private void Update(){
        if (this.isActiveAndEnabled){
            packageMove();
        }
    }

    private void packageMove(){
        float speed = GameManager.Instance.GetPackageSpeed();
        transform.position += transform.forward * speed  * Time.deltaTime;
    }

    //Set Receive Point for object
    public void SetReceivePoint(Transform _receivePoint, string _pointName){
        receivePoint = _receivePoint;
        pointName = _pointName;

        //the name will be set directly to the name visual
        packageVisualUI.SetReceivePointName(_pointName);

    }

    public string GetReceivePointName(){
        return pointName;
    }

    public Transform GetReceivePoint(){
        return receivePoint;
    }

    public void ClearReceivePoint(){
        receivePoint = null;
        pointName = null;
    }
    public void TransformPositionToNextPoint(Transform _nextPoint){
        //Move to next point
        transform.position = _nextPoint.position;
        //Show effect
        ShowParticleEffect();
    }

    public void TransformPositionToNextPosition(Vector3 _nextPosition){
        transform.position = _nextPosition;
    }

    //normal pack doesn't have any special effect
    public void ActiveSpecialEffect(){}


    public void ShowParticleEffect(){
        particleEffect.SetActive(true);
    }
    public void hideParticleEffect(){
        particleEffect.SetActive(false);
    }

    public void Show(){
        this.gameObject.SetActive(true);
        hideParticleEffect();
    }
    public void Hide(){
        this.gameObject.SetActive(false);
    }
}
