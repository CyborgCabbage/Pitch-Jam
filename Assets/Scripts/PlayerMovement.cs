using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;

    [SerializeField] PhysicsMaterial2D bounceMaterial;

    [SerializeField] float moveSpeed;
    [SerializeField] float ballSpeed;
    [SerializeField] float minBallSpeedForSwitch;

    Vector2 ballTrajectory;
    Vector2 playerInput;
    bool isBall = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get inputs
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");

        //Check for roll button
        if (Input.GetButtonDown("Fire1")) //LMB
        {
            //If they are running - turn into ball
            if (!isBall)
            {
                //Make player bounce off bounds
                rb.sharedMaterial = bounceMaterial;

                //Get player direction to mouse direction for trajectory
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 diff = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
                diff.Normalize();
                ballTrajectory = diff;

                //Shoot player
                rb.velocity = new Vector2(ballTrajectory.x * ballSpeed, ballTrajectory.y * ballSpeed);

                isBall = true;
            }
            else
            {
                //Manual ball cancel, start running and dont bounce
                rb.sharedMaterial = null;
                isBall = false;
            }
        }

        if (isBall && rb.velocity.magnitude < minBallSpeedForSwitch)
        {
            isBall = false;
        }
    }

    private void FixedUpdate()
    {
        //Normal walk move
        if (!isBall)
        {
            rb.velocity = new Vector2(playerInput.x * moveSpeed * Time.fixedDeltaTime, playerInput.y * moveSpeed * Time.fixedDeltaTime);
        }
      
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
