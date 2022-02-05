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
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();

        }

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

     void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        music.Stop();
        if (scene.buildIndex == 0) { music.clip = startClip; }
        if (scene.buildIndex == 1) { music.clip = gameClip;  }
        if (scene.buildIndex == 2) { music.clip = endClip;   }
        music.loop = true;
        music.Play();
    }

}
