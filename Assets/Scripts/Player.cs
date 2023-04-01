using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public static int score;
    [SerializeField]float health;
    [SerializeField] Goal[] goals;
    int targetGoal;
    public bool inLight = true;
    float radius = 1;
    [SerializeField]float damage = 1;
    float time = 1;
    public bool isAlive = true;

    public void Update()
    {
        inLight = IsInLight();
        if (!inLight)
        {
            DecreaseHealth(damage * Time.deltaTime);
        }

        if(inLight && health < 5)
        {
            IncreaseHealth(damage * Time.deltaTime);
        }

        if(health <= 0)
        {
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
        for (int i = 0; i < goals.Length; i++)
        {
           goals[i].SwapActiveGoals();
        }
    }

    public void DecreaseHealth(float decrease)
    {
        if (time <= 0)
        {
            health -= decrease;
        }
    }

    public void IncreaseHealth(float increase)
    {
        health += increase;
    }

    private bool IsInLight()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("CircleLight");
        foreach (GameObject o in objects)
        {
            Light2D light2d = o.GetComponent<Light2D>();
            float distance = (transform.position - o.transform.position).magnitude;
            float strength = light2d.shapeLightFalloffSize + radius;
            if (distance < strength)
            {
                return true;
            }
        }
        return false;
    }
}
