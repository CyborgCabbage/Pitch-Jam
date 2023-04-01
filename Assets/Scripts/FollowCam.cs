using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] PlayerMovement player;
    [SerializeField] float distanceFromPlayerMultiplier;
    [SerializeField] bool focusPlayer = false;

    private void FixedUpdate()
    {
        //If player isnt moving + focus setting is off - dont move to player, stay in position
        if (player.GetVelocity() == Vector3.zero && !focusPlayer) { return; }

        //Move towards the direction the player is going
        Vector3 targetPos = player.transform.position + player.GetVelocity() * distanceFromPlayerMultiplier; // 0.4
        Vector2 diff = targetPos - transform.position;
        transform.Translate(diff * moveSpeed * Time.fixedDeltaTime);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position += new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return 0;
        }
    }
}
