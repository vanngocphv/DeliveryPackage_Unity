using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event EventHandler<OnLineNeedRenderArgs> OnLineNeedRender;
    public class OnLineNeedRenderArgs: EventArgs {
        public Transform transformObject;
    }
    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private LayerMask connectLayer;
    

    private void Awake(){
        Instance = this;
    }

    private void Update(){
        if (GameManager.Instance.IsGamePlaying()){
            Vector3 mousePositionInGame = GetMousePosition();

            if (Mouse.current.leftButton.wasPressedThisFrame){
                Ray ray = Camera.main.ScreenPointToRay(mousePositionInGame);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, connectLayer)){
                    //If ray cast cast success, the hit info has data, send to subr to check and setting line
                    OnLineNeedRender?.Invoke(this, new OnLineNeedRenderArgs{
                        transformObject = hitInfo.transform
                    });
                }
            }
        }
        

    }

    private void LateUpdate() {
    }

    public Vector3 GetMousePosition(){
        Vector3 worldMousePosition = Mouse.current.position.ReadValue();
        worldMousePosition.z = Camera.main.nearClipPlane;

        //Vector3 mousePositionInGame = Camera.main.ScreenToWorldPoint(worldMousePosition);

        return worldMousePosition;
    }
}
