using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Scene scene;
    public float autoLoadNextLevel;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (autoLoadNextLevel == 0)
        {
            Debug.Log("Level auto load disabled!");
        }
        else if (autoLoadNextLevel < 0)
        {
            Debug.Log("Level auto load disabled," +
                "use a positive number in seconds");
        }
        else
        {
            Invoke(nameof(LoadStart), autoLoadNextLevel);
        }
    }

    private void LoadStart()
    {
        LoadLevel("01a Start");
    }
    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested: "+name);
        StartCoroutine(LoadYourAsyncScene(name));
    }
    IEnumerator LoadYourAsyncScene(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
    public void QuitGame()
    {
        Debug.Log("Quit Game Requested.");
        Application.Quit();
    }



}
