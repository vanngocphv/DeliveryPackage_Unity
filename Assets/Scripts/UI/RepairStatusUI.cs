using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStatusUI : MonoBehaviour
{
    private const string CONST_IS_REPAIR = "isRepair";
    [SerializeField] DeliveryConnect connectConveyor;
    
    private Animator anim;

    private void Awake(){
        anim = GetComponent<Animator>();
    }
    private void Start(){
        connectConveyor.OnRepairConveyor += OnRepairUI;
        HideGameObject();
    }


    private void OnRepairUI(object sender, DeliveryConnect.OnRepairConveyorArgs e){
        if (!e.isCompleteRepair){
            ShowGameObject();
        }
        else{
            HideGameObject();
        }
    }


    private void ShowGameObject(){
        anim.SetTrigger(CONST_IS_REPAIR);
        this.gameObject.SetActive(true);
    }
    private void HideGameObject(){
        this.gameObject.SetActive(false);
    }
}
