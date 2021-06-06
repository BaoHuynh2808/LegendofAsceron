using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScripts : MonoBehaviour {
    public AudioMixer audioMixer;
    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider voiceVolSlider;
    public Slider sfxVolSlider;

    public Toggle fullscreenToggle;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        AudioInit();
        VideoInit();
    }
    
    private void AudioInit()
    {
        masterVolSlider.value = PlayerPrefs.GetFloat("masterVol", 0);
        musicVolSlider.value = PlayerPrefs.GetFloat("musicVol", 0);
        voiceVolSlider.value = PlayerPrefs.GetFloat("voiceVol", 0);
        sfxVolSlider.value = PlayerPrefs.GetFloat("sfxVol", 0);
    }

    private void VideoInit()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        SetResolution(PlayerPrefs.GetInt("resolutionIndex", 0));

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = PlayerPrefs.GetString("fullscreenMode", "true") == "true" ? true : false;

        SetFullscreen(fullscreenToggle.isOn);
    }

    // Audio Options - Truong Dang
    public void SetMasterVolume (float volume)
    {
        SetVolume("masterVol", volume);
    }

    public void SetMusicVolume(float volume)
    {
        SetVolume("musicVol", volume);
    }

    public void SetVoiceVolume(float volume)
    {
        SetVolume("voiceVol", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetVolume("sfxVol", volume);
    }

    private void SetVolume(string channel, float volume)
    {
        audioMixer.SetFloat(channel, volume);
        PlayerPrefs.SetFloat(channel, volume);
    }

    // Display Options - Truong Dang
    public void ApplyDisplay()
    {
        SetFullscreen(fullscreenToggle.isOn);
        SetResolution(resolutionDropdown.value);
    }

    void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetString("fullscreenMode", isFullscreen.ToString());
    }

    void SetResolution(int resolutionIndex)
    {
        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenToggle.isOn);
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }
}
