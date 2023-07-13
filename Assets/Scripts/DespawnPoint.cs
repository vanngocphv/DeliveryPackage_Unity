using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        other.transform.position = Vector3.zero;
        other.transform.rotation = new Quaternion(0,0,0,0);
        other.gameObject.SetActive(false);
    }
}
