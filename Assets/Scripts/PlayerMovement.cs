using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    bool inLight = true;
    float radius = 1;

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
                rb.velocity = ballTrajectory * ballSpeed;

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
        //Is in light
        inLight = IsInLight();
        //Normal walk move
        if (!isBall)
        {
            rb.velocity = playerInput.normalized*moveSpeed;
        }
      
    }

    private bool IsInLight() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("CircleLight");
        foreach (GameObject o in objects) {
            Light2D light2d = o.GetComponent<Light2D>();
            float distance = (transform.position - o.transform.position).magnitude;
            float strength = light2d.shapeLightFalloffSize + radius;
            if (distance < strength) {
                return true;
            }
        }
        return false;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
