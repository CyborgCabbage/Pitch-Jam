using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMotion : MonoBehaviour
{
    [SerializeField] Vector2 oscillationDimensions;
    [SerializeField] Vector2 oscillationFrequencies;
    Vector2 origin;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position = origin + new Vector2(Mathf.Cos(oscillationFrequencies.x*time)*oscillationDimensions.x, Mathf.Sin(oscillationFrequencies.y * time) * oscillationDimensions.y);
    }
}
