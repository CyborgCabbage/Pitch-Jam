using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator ani;
    [SerializeField] PlayerMovement playerMov;
    [SerializeField] Rigidbody2D rigbod;

    string aniState = "Idle";

    // Start is called before the first frame update
    void Start()
    {

    }

    void setAnimation(string stateName)
    {
        if (stateName != aniState)
        {
            ani.Play(stateName, 0, 0);

            aniState = stateName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rigbod.velocity.magnitude > 0.2f)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                setAnimation("Run");
            }
        }
        else
        {
            setAnimation("Idle");
        }
    }
}
