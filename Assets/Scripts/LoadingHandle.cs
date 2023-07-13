using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHandle : MonoBehaviour
{
    private bool callBackLoading = false;

    private void Start(){
        callBackLoading = false;
    }

    // Update is called once per frame
    void Update() {
        if (callBackLoading == false){
            callBackLoading = true;
            LoadingStaticClass.LoadingCallback();
        }
    }
}
