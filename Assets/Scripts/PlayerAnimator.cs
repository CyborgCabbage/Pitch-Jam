using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator ani;
    [SerializeField] PlayerMovement playerMov;
    [SerializeField] Rigidbody2D rigbod;

    Vector2 playerInput;
    string aniState = "Idle";
    float referenceScale;
    [SerializeField]  bool hasSetBallDir = false;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");


        if (true)
        {
            if (!hasSetBallDir)
            {
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
            if (playerInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1) * referenceScale;
            }
            else if (playerInput.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1) * referenceScale;
            }
            hasSetBallDir = false;

            if (playerInput.magnitude > 0)
            {
                if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle") || ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.1f)
                {
                    setAnimation("Run", true);
                }
            }
            else
            {
                if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Run") || ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.1f)
                {
                    setAnimation("Idle", false);
                }
            }
        }
    }
}
