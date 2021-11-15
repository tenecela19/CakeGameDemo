using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SceneChange(string nameScene)
    {
        StartCoroutine(LoadAsynchrnously(nameScene));
    }
    IEnumerator LoadAsynchrnously (string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
