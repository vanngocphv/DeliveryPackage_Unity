using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawnPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> listPackage;
    [SerializeField] private List<GameObject> listBomb;
    [SerializeField] private int rngMin = 1;
    [SerializeField] private int rngMax = 7;
    private int randomRng;
    private int countCurrentPackage;
    private int countCurrentBomb;

    private void Start(){
        countCurrentPackage = 0;
        countCurrentBomb = 0;
        DeliveryManager.Instance.OnDeliverySpawn += OnSpawnStart;
        
        //Roll for spawn Bomb chance
        randomRng = Random.Range(rngMin, rngMax);
    }

    private void OnSpawnStart(object sender, DeliveryManager.OnDeliverySpawnArgs e){
        if (e.currentSpawnPoint == this){
            //Roll for spawn Bomb chance
            int rng = Random.Range(rngMin, rngMax);

            int count = -1;
            IF_DeliveryContent package = null;
            if (rng == randomRng){
                //Spawn bomb Package;
                package = GetSpawnPackage(listBomb, ref countCurrentBomb, out countCurrentBomb);
                count = countCurrentBomb;
            }
            else{
                //Spawn normal package
                package = GetSpawnPackage(listPackage, ref countCurrentPackage, out countCurrentPackage);
                count = countCurrentPackage;
            }
            package.TransformPositionToNextPosition(this.transform.position + Vector3.up/2);
            package.SetReceivePoint(e.packagePoint.pointReceive, e.packagePoint.pointName);
            package.Show();
        }
    }

    private IF_DeliveryContent GetSpawnPackage(List<GameObject> _listGameObject,ref int currentCount ,out int countPack){
        int count = currentCount;
        IF_DeliveryContent package = null;
        if (_listGameObject[count].TryGetComponent(out IF_DeliveryContent packDelivery)){
            package = packDelivery;
        };
        currentCount++;
        if (currentCount > _listGameObject.Count - 1) currentCount = 0;
        countPack = currentCount;
        return package;
        

    }
}
