using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PackageVisualUI : MonoBehaviour
{
    [SerializeField] Package mainPackage;
    [SerializeField] private TextMeshProUGUI receivePointName;


    public void SetReceivePointName(string pointName){
        receivePointName.text = pointName;
    }

}
