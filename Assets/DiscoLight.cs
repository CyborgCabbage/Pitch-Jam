using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DiscoLight : MonoBehaviour
{
    Color baseColor;
    float offset;
    float time;
    private void Start()
    {
        offset = Random.Range(0.0f, 1.0f);
        baseColor = GetComponent<Light2D>().color;
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("disco") == 1)
        {
            time += Time.deltaTime;
            GetComponent<Light2D>().color = Color.HSVToRGB((offset + time) % 1.0f, 1, 1);
        }
        else {
            GetComponent<Light2D>().color = baseColor;
        }
    }
}
