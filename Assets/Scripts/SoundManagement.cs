using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public bool isSoundOn;
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        if (isSoundOn)
        {
            soundOnButton.SetActive(true);
            soundOffButton.SetActive(false);
            audioSource.Play();
        }
        else
        {
            soundOnButton.SetActive(false);
            soundOffButton.SetActive(true);
            audioSource.Stop();
        }
    }
    public void PauseMusic()
    {
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);
        isSoundOn = true;
        audioSource.Pause();
        PlayerPrefs.SetInt("Sound", 0);
    }
    public void PlayMusic()
    {
        soundOffButton.SetActive(false);
        soundOnButton.SetActive(true);
        isSoundOn = false;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.UnPause();
        }
        PlayerPrefs.SetInt("Sound", 1);
    }
}
