using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Toggle camShakeToggle;
    [SerializeField] Toggle discoToggle;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] float defaultSFXVolume;
    [SerializeField] float defaultMusicVolume;

    private void Start()
    {
        //Disco
        if (!PlayerPrefs.HasKey("camShake"))
        {
            PlayerPrefs.SetInt("camShake", 0);
        }
        camShakeToggle.isOn = (PlayerPrefs.GetInt("camShake") == 1);
        //SFX Slider
        if (!PlayerPrefs.HasKey("sfxVolume")) 
        {
            //Set default
            PlayerPrefs.SetFloat("sfxVolume", defaultSFXVolume);
        }
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        //Music Slider
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            //Set default
            PlayerPrefs.SetFloat("musicVolume", defaultMusicVolume);
        }
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //Disco
        if (!PlayerPrefs.HasKey("disco"))
        {
            PlayerPrefs.SetInt("disco", 0);
        }
        discoToggle.isOn = (PlayerPrefs.GetInt("disco") == 1);
        
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
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }

    public void SetDisco(bool d) {
        PlayerPrefs.SetInt("disco", d ? 1 : 0);
        PlayerPrefs.Save();
    }
}
