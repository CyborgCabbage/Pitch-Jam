using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;

    [SerializeField] float moveSpeed;
    [SerializeField] float ballSpeed;

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
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isBall)
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 diff = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
                diff.Normalize();
                ballTrajectory = diff;
                rb.velocity = new Vector2(ballTrajectory.x * ballSpeed, ballTrajectory.y * ballSpeed);

                isBall = true;
            }
            else
            {
                isBall = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Normal walk move
        if (!isBall)
        {
            rb.velocity = new Vector2(playerInput.x * moveSpeed * Time.fixedDeltaTime, playerInput.y * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
           // rb.velocity = new Vector2(ballTrajectory.x * ballSpeed * Time.fixedDeltaTime, ballTrajectory.y * ballSpeed * Time.fixedDeltaTime);
        }
    }
}
