using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    public static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("Duplicate music player self-destructing!");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();

        }

    }

    // Use this for initialization
    void Start ()
    {
        
        
    }

     void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
     void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        Debug.Log("Music player loaded level"+ scene.buildIndex.ToString());
        music.Stop();
        if (scene.buildIndex == 0) { music.clip = startClip; }
        if (scene.buildIndex == 1) { music.clip = gameClip;  }
        if (scene.buildIndex == 2) { music.clip = endClip;   }
        music.loop = true;
        music.Play();
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
