using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Toggle camShakeToggle;

    private void Start()
    {
        if (PlayerPrefs.GetInt("camShake") == 0)
        {
            camShakeToggle.isOn = false;
        }
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
}
