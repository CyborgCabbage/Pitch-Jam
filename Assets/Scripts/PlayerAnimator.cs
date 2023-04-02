using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] AudioSource audioScr;
    [SerializeField] Animator ani;
    [SerializeField] PlayerMovement playerMov;
    [SerializeField] Rigidbody2D rigbod;
    [SerializeField] List<AudioClip> sfx;

    string aniState = "Idle";
    float referenceScale;
    [SerializeField]  bool hasSetBallDir = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            audioScr.volume = PlayerPrefs.GetFloat("sfxVolume") * audioScr.volume;
        }
        referenceScale = transform.localScale.x;
    }

    void setAnimation(string stateName, bool force)
    {
        if (stateName != aniState || force)
        {
            ani.Play(stateName, 0, 0);

            aniState = stateName;
        }
    }

    float getAniTime()
    {
        return ani.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    // Update is called once per frame
    void Update()
    {
        // sets animations depending on various states (moving, still, in-ball, transitioning from ball to normal mode)
        // and plays sounds too

        if (playerMov.isBall)
        {
            if (!hasSetBallDir)
            {
                audioScr.PlayOneShot(sfx[1]);
                if (rigbod.velocity.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1) * referenceScale;
                }
                else if (rigbod.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1) * referenceScale;
                }
                hasSetBallDir = true;
            }

            setAnimation("Roll", false);
            ani.speed = rigbod.velocity.magnitude / 4;
        }
        else
        {
            if (playerMov.playerInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1) * referenceScale;
            }
            else if (playerMov.playerInput.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1) * referenceScale;
            }
            hasSetBallDir = false;


            if (aniState == "Roll" || (aniState == "OutOfBall" && getAniTime() < 0.85f))
            {
                setAnimation("OutOfBall", false);
            }
            else if (playerMov.playerInput.magnitude > 0)
            {
                if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle") || getAniTime() >= 1.1f)
                {
                    audioScr.PlayOneShot(sfx[0]);
                    setAnimation("Run", true);
                }
            }
            else
            {
                if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Run") || getAniTime() >= 1.1f)
                {
                    setAnimation("Idle", false);
                }
            }
        }
    }
}
