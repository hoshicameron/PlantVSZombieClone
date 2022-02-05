using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectController : MonoBehaviour
{
    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public  void PlaySoundEffect()
    {
        //Play audio clip
        audioSource.PlayOneShot(audioSource.clip);

        // Disable game object after delay
        StartCoroutine(DisableGameObject(audioSource.clip.length));
    }

    public IEnumerator DisableGameObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
