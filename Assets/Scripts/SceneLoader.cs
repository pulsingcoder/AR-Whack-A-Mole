using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private string sceneNameToBeLoaded;
   public void LoadScene(string _sceneName) 
   {
        sceneNameToBeLoaded = _sceneName;
        StartCoroutine(InitialiseSceneLoading());
   }


    IEnumerator InitialiseSceneLoading()
    {
        // first we load the loading scene
        yield return SceneManager.LoadSceneAsync("Scene_Loading");

        // load actual scene
        StartCoroutine(LoadActualScene());
    }


    IEnumerator LoadActualScene()
    {
        var asyncSceneLoading = SceneManager.LoadSceneAsync(sceneNameToBeLoaded);
        // this value stop the scene for loading it's content until scene is loaded 100%
        asyncSceneLoading.allowSceneActivation = false;

        while(!asyncSceneLoading.isDone)
        {
            if (asyncSceneLoading.progress >=0.9f)
            {
                // load the scene
                asyncSceneLoading.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}
