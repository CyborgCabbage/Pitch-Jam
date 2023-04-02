using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    [SerializeField]Player player;
    [SerializeField] Image image;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.lightCount > 0)
        {
            StartCoroutine(FadeBlackOutSquare(.25f, false));
        }
        else
        {
            StartCoroutine(FadeBlackOutSquare(.25f));
        }
        StopAllCoroutines();
    }

    public IEnumerator FadeBlackOutSquare(float fadeSpeed, bool fadeToBlack = true)
    {
        Color objectColour = image.color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (image.color.a < 1)
            {
                fadeAmount = objectColour.a + (fadeSpeed * Time.deltaTime);
                objectColour = new Color(objectColour.r, objectColour.g, objectColour.b, fadeAmount);
                image.color = objectColour;
                yield return null;
            }
        }
        else
        {
            while(image.color.a > 0)
            {
                fadeAmount = objectColour.a - (fadeSpeed * Time.deltaTime);
                objectColour = new Color(objectColour.r, objectColour.g, objectColour.b, fadeAmount);
                image.color = objectColour;
                yield return null;
            }
        }
    }
}
