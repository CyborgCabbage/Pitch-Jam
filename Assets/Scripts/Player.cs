using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public static int score;
    [SerializeField]float maxHealth = 5;
    float health;
    [SerializeField] Goal[] goals;
    int targetGoal;
    float radius = 1;
    [SerializeField]float damage = 1;
    float time = 1;
    public bool isAlive = true;
    public int lightCount = 0;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioSource music;
    [SerializeField] List<AudioClip> sfx;

    private void Start()
    {

        health = maxHealth;
        score = 0;
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            audio.volume = PlayerPrefs.GetFloat("sfxVolume") * audio.volume;
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            music.volume = PlayerPrefs.GetFloat("musicVolume") * music.volume;
        }
        if(PlayerPrefs.GetInt("disco") == 0)
        {
            music.clip = sfx[1];
            music.Play();
        }
        if(PlayerPrefs.GetInt("disco") == 1)
        {
            music.clip=sfx[2];
            music.Play();
        }
    }

    public float GetHealthFraction() {
        return Mathf.Clamp01(health / maxHealth);
    }

    public void Update()
    {
        if (lightCount == 0)
        {
            DecreaseHealth(damage * Time.deltaTime);
        }

        if(lightCount > 0 && health < maxHealth)
        {
            IncreaseHealth(damage * Time.deltaTime);
        }

        if(health <= 0)
        {
            health = 0;
            isAlive = false;
        }

        if (!isAlive)
        {
            GetComponent<PlayerMovement>().enabled = false;
        }

        time -= Time.deltaTime;
    }

    public void SetGoals(bool open)
    {
        for (int i = 0; i < goals.Length; i++)
        {
            if (goals[i].GetIsScorable())
            {
                goals[i].SetOpen(open);
            }
        }
    }

    public void IncrementGoals()
    {
        score++;
        audio.PlayOneShot(sfx[0]);
        for (int i = 0; i < goals.Length; i++)
        {
           goals[i].SwapActiveGoals();
        }
    }

    public void DecreaseHealth(float decrease)
    {
        if (time <= 0)
        {
            float multiplier = 1 + (Player.score / 10f);
            multiplier = Mathf.Clamp(multiplier, 1, 5);

            health -= decrease * multiplier;
        }
    }

    public void IncreaseHealth(float increase)
    {
        health += increase;
    }
}
