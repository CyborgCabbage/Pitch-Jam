using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Toggle camShakeToggle;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] float defaultSFXVolume;
    [SerializeField] float defaultMusicVolume;

    private void Start()
    {
        if (PlayerPrefs.GetInt("camShake") == 0)
        {
            camShakeToggle.isOn = false;
        }
        if (!PlayerPrefs.HasKey("sfxVolume")) 
        {
            //Set default
            PlayerPrefs.SetFloat("sfxVolume", defaultSFXVolume);
        }
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            //Set default
            PlayerPrefs.SetFloat("musicVolume", defaultMusicVolume);
        }
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void ToggleCamShake()
    {
        if (camShakeToggle.isOn)
        {
            PlayerPrefs.SetInt("camShake", 1);
        }
        else
        {
            PlayerPrefs.SetInt("camShake", 0);
        }
        PlayerPrefs.Save();
    }

    public void ChangeSFXVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    public void ChangeMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
