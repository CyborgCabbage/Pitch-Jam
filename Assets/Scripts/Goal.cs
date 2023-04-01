using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] bool isActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if goal is scorable (cant score in same goal twice)
        if (!isActive) { return; }

        //Check if collision is player
        Player player = collision.GetComponent<Player>();
        if (!player) { return; }

        player.IncrementGoals();

    }

    public void SwapActiveGoals()
    {
        if (isActive) { isActive = false; }
        else { isActive = true; }
    }
}
