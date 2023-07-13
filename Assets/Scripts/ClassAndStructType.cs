using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassAndStructType : MonoBehaviour
{
    [Serializable]
    public struct ReceivePointType{
        public Transform pointReceive;
        public string pointName;
    }
}
