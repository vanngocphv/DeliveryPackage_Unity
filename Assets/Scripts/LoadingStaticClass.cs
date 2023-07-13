using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadingStaticClass
{
    public enum Scene{
        MenuScene,
        LoadingScene,
        GameScene
    }

    private static Scene targetScene;
    public static void SetTargetScene(Scene _targetScene){
        LoadingStaticClass.targetScene = _targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoadingCallback(){
        //The loading has been called success
        SceneManager.LoadScene(LoadingStaticClass.targetScene.ToString());
    }
}
