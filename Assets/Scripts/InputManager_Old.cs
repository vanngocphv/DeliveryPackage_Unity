using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager_Old : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private LayerMask connectLayer;
    private LineRenderer lr;
    private int lrPositionCount = 2;
    //Trail, position 1 of the line renderer
    private TrailConnect oldTrailConnect;
    private Vector3 oldTrailPosition;
    //Delivery, position 2 of the line renderer
    private DeliveryConnect oldDeliveryConnect;
    private Vector3 oldDeliveryPosition;

    private bool isHasPosition2 = true;

    private void Awake(){

    }

    private void Update(){
        Vector3 worldClickPosition = Mouse.current.position.ReadValue();
        worldClickPosition.z = Camera.main.nearClipPlane;

        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(worldClickPosition);
        if (Mouse.current.leftButton.wasPressedThisFrame){

            Ray ray = Camera.main.ScreenPointToRay(worldClickPosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, connectLayer)){
                if (lr != null && isHasPosition2 == true){
                    Destroy(lr);
                    isHasPosition2 = false;
                }
                
                //Trail Connect
                if (hitInfo.transform.TryGetComponent<TrailConnect>(out TrailConnect trailConnect)){
                    Debug.Log(trailConnect);
                    if (trailConnect.gameObject.TryGetComponent(out LineRenderer line)){
                        Destroy(line);
                    }
                    
                    //If linerenderer doesn't exist, create new with position 0 is the click position
                    if (lr == null){
                        lr = trailConnect.gameObject.AddComponent<LineRenderer>();
                        lr.positionCount = lrPositionCount;
                        lr.SetPosition(0, trailConnect.transform.position);
                        
                        oldTrailPosition = trailConnect.transform.position;
                        //not have position 2 in the line
                        isHasPosition2 = false;
                    }
                    else{
                        if (oldTrailPosition != trailConnect.transform.position){
                            lr.SetPosition(1, trailConnect.transform.position);

                            //Has position 2 in the line
                            isHasPosition2 = true;
                        }
                    }
                }
                //Delivery Connect
                else if (hitInfo.transform.TryGetComponent<DeliveryConnect>(out DeliveryConnect deliveryConnect)){
                    Debug.Log(deliveryConnect);
                    if (deliveryConnect.gameObject.TryGetComponent(out LineRenderer line)){
                        Destroy(line);
                    }
                    //If linerenderer doesn't exist, create new with position 0 is the click position
                    if (lr == null){
                        lr = deliveryConnect.gameObject.AddComponent<LineRenderer>();
                        lr.positionCount = lrPositionCount;
                        lr.SetPosition(0, deliveryConnect.transform.position);

                        oldDeliveryPosition = deliveryConnect.transform.position;

                        //not have position 2 in the line
                        isHasPosition2 = false;
                    }
                    else {
                        if (deliveryConnect.transform.position != oldDeliveryPosition){
                            lr.SetPosition(1, deliveryConnect.transform.position);
                            
                            //Has position 2
                            isHasPosition2 = true;
                        }
                    }
                }
            }
        }

        //If line renderer is existed, add position 1 is the current mouse position with worldspace same with
        //the position of scene level
        if (lr != null && isHasPosition2 == false){
            lr.SetPosition(1, clickPosition);
        }

    }

    private void LateUpdate() {
    }
}
