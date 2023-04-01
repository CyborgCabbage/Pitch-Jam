using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;

    [SerializeField] PhysicsMaterial2D bounceMaterial;
    [SerializeField] Player player;
    [SerializeField] ParticleSystem rollParticles;

    [SerializeField] float walkDrag;
    [SerializeField] float ballDrag;
    [SerializeField] float maxSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float ballSpeed;
    [SerializeField] float minBallSpeedForSwitch;

    [SerializeField] float camShakeDuration;
    [SerializeField] float camShakeMagnitude;

    Vector2 ballTrajectory;
    public Vector2 playerInput;
    public bool isBall = false;
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

                //Get player direction to mouse direction for trajectory
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 diff = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
                diff.Normalize();
                ballTrajectory = diff;

                Roll(ballTrajectory, ballSpeed);
            }
        }

        if (isBall && rb.velocity.magnitude < minBallSpeedForSwitch)
        {
            player.SetGoals(false);
            rb.sharedMaterial = null;
            rb.drag = walkDrag;
	        isBall = false;
            rollParticles.Stop();
        }
    }

    private void FixedUpdate()
    {
        //Is in light
        
        //Normal walk move
        if (!isBall)
        {
           rb.velocity += new Vector2(playerInput.x * moveSpeed * Time.fixedDeltaTime, playerInput.y * moveSpeed * Time.fixedDeltaTime);

           rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
        }
      
    }
    
    public void Roll(Vector2 trajectory, float force)
    {
        player.SetGoals(true);
        rollParticles.Play();
        StartCoroutine(cam.GetComponent<FollowCam>().Shake(camShakeDuration, force * 0.008f));

        //Make player bounce off bounds
        rb.sharedMaterial = bounceMaterial;
        rb.drag = ballDrag;

        //Shoot player
        rb.velocity = new Vector2(trajectory.x * force, trajectory.y * force);

        isBall = true;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public bool GetIsRolling()
    {
        return isBall;
    }
    

}
