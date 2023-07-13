using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IF_DeliveryContent
{
    [SerializeField] private DeliveryConnect theAffectDelivery;
    [SerializeField] private GameObject explosionBomb;
    private float countDown;
    private float timeToBomb = 1f;
    private float deactiveCountDown;
    private float timeToDeactive = 0.7f;
    private bool isBomb = false;
    private bool isDeactive = false;

    private void Update(){
        if (this.isActiveAndEnabled){
            packageMove();
        }
    }
    private void FixedUpdate() {
        if (isBomb){
            countDown -= Time.deltaTime;
            if (countDown < 0f){
                Debug.Log("BOMB!");
                isBomb = false;
                isDeactive = true;
                deactiveCountDown = timeToDeactive;
                LineRenderManager.Instance.DestroyCurrentLine();
                ShowParticleEffect();
            }
        }

        if (!isBomb && isDeactive){
            deactiveCountDown -= Time.deltaTime;
            if (deactiveCountDown < 0f){
                //The current line has been bomb => cannot using => change status this line
                theAffectDelivery.ChangeStatus(false);

                //Set the boom reset the location/rotation
                Hide();
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.rotation = Quaternion.identity;
                isDeactive = false;
            }
        }
    }

    private void packageMove(){
        float speed = GameManager.Instance.GetPackageSpeed();
        transform.position += transform.forward * speed  * Time.deltaTime;
    }

    public void SetReceivePoint(Transform _receivePoint, string _pointName){
        if (_receivePoint.gameObject.TryGetComponent(out DeliveryConnect deliveryConnect)){
            theAffectDelivery = deliveryConnect;
        }
    }

    public string GetReceivePointName(){
        return null;
    }

    public Transform GetReceivePoint(){
        return null;
    }

    public void ClearReceivePoint(){

    }

    public void TransformPositionToNextPoint(Transform _nextPoint){
        transform.position = _nextPoint.position;
    }

    public void TransformPositionToNextPosition(Vector3 _nextPosition){
        transform.position = _nextPosition;
    }

    //the Bomb package will bomb after the trigger exit active;
    public void ActiveSpecialEffect(){
        countDown = timeToBomb;
        isBomb = true;
    }

    public void Show(){
        HideParticleEffect();
        this.gameObject.SetActive(true);
    }
    public void Hide(){
        this.gameObject.SetActive(false);
    }

    private void HideParticleEffect(){
        explosionBomb.SetActive(false);
    }
    private void ShowParticleEffect(){
        explosionBomb.SetActive(true);
    }
}
