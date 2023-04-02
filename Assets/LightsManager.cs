using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightsManager : MonoBehaviour
{
    [SerializeField] List<Light2D> removeableLights;
    List<int> remainingLights = new List<int>();
    [SerializeField] int removeEveryXPoints;

    int oldScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < removeableLights.Count; i++)
        {
            remainingLights.Add(i);
        }
    }

    IEnumerator fadeLight(int i)
    {
        float duration = 2f;
        float elapsed = 0f;
        float initialIntensity = removeableLights[i].intensity;

        while (elapsed < duration)
        {
            removeableLights[i].intensity = Mathf.Lerp(initialIntensity, 0, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return 0;
        }

        removeableLights[i].intensity = 0;

        removeableLights[i].gameObject.SetActive(false);
    }

    void removeLight()
    {
        if (remainingLights.Count > 0)
        {
            int i = remainingLights[Random.Range(0, remainingLights.Count)];
            remainingLights.Remove(i);

            StartCoroutine(fadeLight(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.score > oldScore + removeEveryXPoints - 1)
        {
            removeLight();
            oldScore = Player.score;
        }
    }
}
