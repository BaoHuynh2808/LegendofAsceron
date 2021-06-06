using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public AudioSource bgmSource;
    public AudioClip[] normalBgms;
    public AudioClip[] battleBgms;
    public AudioClip[] bossBgms;

    public AudioSource sfxSource;
    public AudioClip[] buttonSFX;

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
            Initialize();
            DontDestroyOnLoad(this);
        }
    }

    private void Initialize()
    {
        SetVolume("masterVol", PlayerPrefs.GetFloat("masterVol", 0));
        SetVolume("musicVol", PlayerPrefs.GetFloat("musicVol", 0));
        SetVolume("voiceVol", PlayerPrefs.GetFloat("voiceVol", 0));
        SetVolume("sfxVol", PlayerPrefs.GetFloat("sfxVol", 0));
    }

    private void SetVolume(string channel, float volume)
    {
        audioMixer.SetFloat(channel, volume);
    }

    public void ChangeBgm(AudioClip bgm)
    {
        bgmSource.Stop();
        bgmSource.clip = bgm;
        bgmSource.Play();
    }

    public void PauseBGM(bool isPause)
    {
        if (isPause)
        {
            bgmSource.Pause();
        }
        else
        {
            bgmSource.UnPause();
        }
    }

    public void PlayButtonSFX(int index)
    {
        PlaySFX(buttonSFX[index]);
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
