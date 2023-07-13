using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    private bool isClickButton;

    private void Awake(){

    }

    private void Start(){

    }

    private void Update(){
        
    }

    private void LateUpdate(){
        transform.forward = Camera.main.transform.forward;
    }

    
}
