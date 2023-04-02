using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CircleLight : MonoBehaviour
{
    [SerializeField] Vector2 oscillationDimensions;
    [SerializeField] Vector2 oscillationFrequencies;
    Vector2 origin;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        Light2D light = GetComponent<Light2D>();
        collider.radius = 0.5f+light.shapeLightFalloffSize;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position = origin + new Vector2(Mathf.Cos(oscillationFrequencies.x*time)*oscillationDimensions.x, Mathf.Sin(oscillationFrequencies.y * time) * oscillationDimensions.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;
        player.lightCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player == null) return;
        player.lightCount--;
    }
}
