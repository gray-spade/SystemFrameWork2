using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType {
    Title,
    Lobby,
    InGame,

}


public class SceneLoader : SingetonBehaviour<SceneLoader>
{
    public void LoadScene(SceneType sceneType) {
        Logger.Log($"{sceneType} scene Loading");

        Time.timeScale = 1f;


        SceneManager.LoadScene(sceneType.ToString());
    }

    public void ReloadScene() {
        Logger.Log($"{SceneManager.GetActiveScene().name} scene Loading");

        Time.timeScale = 1f;


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public AsyncOperation LoadSceneAsync(SceneType sceneType)
    {
        Logger.Log($"{sceneType} scene Async( Loading");

        Time.timeScale = 1f;


        return SceneManager.LoadSceneAsync(sceneType.ToString());
    }
}
