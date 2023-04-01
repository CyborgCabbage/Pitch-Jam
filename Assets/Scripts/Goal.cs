using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] bool isScorable;
    BoxCollider2D box;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if goal is scorable (cant score in same goal twice)
        if (!isScorable) { return; }

        //Check if collision is player
        Player player = collision.GetComponent<Player>();
        if (!player) { return; }

        player.IncrementGoals();

    }

    public void SwapActiveGoals()
    {
        if (isScorable) { isScorable = false; }
        else { isScorable = true; }
    }


    public void SetOpen(bool isOpen)
    {
        if (isOpen)
        {
            box.isTrigger = true;
        }
        else
        {
            box.isTrigger = false;
        } 
    }

    public bool GetIsScorable()
    {
        return isScorable;
    }
}
