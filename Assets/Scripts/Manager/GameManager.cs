using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnGameCountDown;

    public enum GameState{
        EarlyLoading,           //Loading some initial object
        MainLoading,            //Loading main object
        Playing,                //In state Playing
        GameOver,               //Lose State
        GameWin,                //Win state
    }
    //State
    private GameState currentState;
    private float timeCount;

    [SerializeField] private int maxReceivePackageNeeded;
    [SerializeField] private int minReceivePackageNeeded;
    [SerializeField] private int receivePackageNeeded;
    
    [SerializeField] private float packageSpeed = 3f;

    [SerializeField] private float spawnPackageTimeMax = 2f;
    [SerializeField] private float spawnPackageTimeMin = 0.2f;

    // Point for Package
    [SerializeField] private List<PackageSpawnPool> listDeliverySpawnPoint;
    [SerializeField] private List<ClassAndStructType.ReceivePointType> listReceivePoint;

    [SerializeField] private float maxTimeRepair = 4f;

    // The game UI;
    [SerializeField] private GameOverUI gameOverUI;

    //Time for game state;
    [SerializeField] private float earlyLoadingTimeMax = 1f;
    [SerializeField] private float mainLoadingTimeMax = 4f;
    [SerializeField] private float maxTimeCanPlay;
    private float earlyLoadingTime;
    private bool isGamePlaying = false;
    private bool isGameOver = false;
    private bool isGameWin = false;

    private void Awake(){
        Instance = this;
        receivePackageNeeded = UnityEngine.Random.Range(minReceivePackageNeeded, maxReceivePackageNeeded);
    }
    private void Start(){
        SetTimeScale(1);
        currentState = GameState.EarlyLoading;
        earlyLoadingTime = earlyLoadingTimeMax;
    }

    private void Update(){
        switch (currentState){
            case GameState.EarlyLoading:
                //this early load, will loading something in there, in this game, maybe just countdown 1s for smoothing everything
                earlyLoadingTime -= Time.deltaTime;
                if (earlyLoadingTime < 0f) {
                    OnGameCountDown?.Invoke(this, EventArgs.Empty);
                    currentState = GameState.MainLoading;
                }
                break;
            
            case GameState.MainLoading:
                //this state is countdown loading
                if (isGamePlaying)
                    currentState = GameState.Playing;
                break;
            
            case GameState.Playing:
                if (isGameOver && !isGameWin){
                    currentState = GameState.GameOver;
                }
                else if (isGameWin && !isGameOver){
                    currentState = GameState.GameWin;
                }
                break;

            case GameState.GameOver:
                gameOverUI.ShowGameObject();
                SetTimeScale(0); // scale time to 0;
                break;

            case GameState.GameWin:
                gameOverUI.ShowGameObject();
                SetTimeScale(0); // scale time to 0;
                break;
        }
    }

    private void FixedUpdate() {
    }

    private void SetTimeScale(int timeScale){
        Time.timeScale = timeScale;
    }

    public int GetPackageNeeded(){
        return receivePackageNeeded;
    }

    public float GetMaxTimeCanPlay(){
        return maxTimeCanPlay;
    }

    public float GetPackageSpeed(){
        return packageSpeed;
    }

    public void GetSpawnTime(out float maxTime, out float minTime){
        maxTime = spawnPackageTimeMax;
        minTime = spawnPackageTimeMin;
    }

    public List<PackageSpawnPool> GetListDelivery(){
        return listDeliverySpawnPoint;
    }

    public List<ClassAndStructType.ReceivePointType> GetListReceivePoint(){
        return listReceivePoint;
    }

    public float GetMaxTimeLoadingMain(){
        return mainLoadingTimeMax;
    }

    public float GetMaxTimeRepair(){
        return maxTimeRepair;
    }
    
    public void ChangeIsGameOver(bool _isGameOver){
        isGameOver = _isGameOver;
    }

    public void ChangeIsGameWin(bool _isGameWin){
        isGameWin = _isGameWin;
    }

    public bool IsGamePlaying(){
        return currentState == GameState.Playing;
    }
    public bool isGameLoadingMain(){
        return currentState == GameState.MainLoading;
    }

    public void ChangeIsGamePlaying(bool _isGamePlaying){
        isGamePlaying = _isGamePlaying;
    }
}