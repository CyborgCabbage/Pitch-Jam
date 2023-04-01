using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int score;
    int health;
    [SerializeField] Goal[] goals;
    int targetGoal;

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

    }
}
