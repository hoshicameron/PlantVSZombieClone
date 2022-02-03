using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't destroy on load: "+ name);
    }

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
        Debug.Log("Plying clip:" + thisLevelMusic);

        if (thisLevelMusic)
        {
            //if there's some music attached
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
