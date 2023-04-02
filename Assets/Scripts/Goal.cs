using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Goal : MonoBehaviour
{
    [SerializeField] bool isScorable;
    BoxCollider2D box;
    [SerializeField] ParticleSystem scoreParticles;
    [SerializeField] Sprite activeGoalSprite;
    [SerializeField] Sprite inactiveGoalSprite;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Light2D goalLight;
    [SerializeField] float activeGoalLighting;
    [SerializeField] float inactiveGoalLighting;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        SetGoalSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if goal is scorable (cant score in same goal twice)
        if (!isScorable) { return; }

        //Check if collision is player
        Player player = collision.GetComponent<Player>();
        if (!player) { return; }


        scoreParticles.Play();
        player.IncrementGoals();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isScorable) { return; }

        //Check if collision is player
        Player player = collision.GetComponent<Player>();
        if (!player) { return; }


        scoreParticles.Play();
        player.IncrementGoals();
    }

    private void SetGoalSprite()
    {
        if (isScorable)
        {
            spriteRenderer.sprite = activeGoalSprite;
            goalLight.intensity = activeGoalLighting;
        }
        else
        {
            spriteRenderer.sprite = inactiveGoalSprite;
            goalLight.intensity = inactiveGoalLighting;
        }
    }

    public void SwapActiveGoals()
    {
        if (isScorable) { isScorable = false; }
        else { isScorable = true; }

        SetGoalSprite();
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
