using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    public event EventHandler<OnDeliverySpawnArgs> OnDeliverySpawn;

    public class OnDeliverySpawnArgs: EventArgs{
        public PackageSpawnPool currentSpawnPoint;
        public ClassAndStructType.ReceivePointType packagePoint;
    }

    private List<PackageSpawnPool> listDeliverySpawnPoint;
    private List<ClassAndStructType.ReceivePointType> listReceivePoint;

    private float spawnTime;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        //set list data
        listDeliverySpawnPoint = GameManager.Instance.GetListDelivery();
        listReceivePoint = GameManager.Instance.GetListReceivePoint();

        GameManager.Instance.GetSpawnTime(out float spawnTimeMax, out float spawnTimeMin);
        spawnTime = UnityEngine.Random.Range(spawnTimeMin, spawnTimeMax);
    }

    private void Update(){
        if (GameManager.Instance.IsGamePlaying()){
            spawnTime -= Time.deltaTime;
            if (spawnTime < 0f){
                GameManager.Instance.GetSpawnTime(out float spawnTimeMax, out float spawnTimeMin);
                spawnTime = UnityEngine.Random.Range(spawnTimeMin, spawnTimeMax);
                //Start Delivery
                SpawnEventStart();
            }
        }
    }

    private void SpawnEventStart(){
        int spawnPoint = UnityEngine.Random.Range(0, listDeliverySpawnPoint.Count);
        int receivePoint = UnityEngine.Random.Range(0, listReceivePoint.Count);
        OnDeliverySpawn?.Invoke(this, new OnDeliverySpawnArgs{
            //Spawn object point
            currentSpawnPoint = listDeliverySpawnPoint[spawnPoint],
            //object want to be received in point
            packagePoint = listReceivePoint[receivePoint]
        });
    }


}
