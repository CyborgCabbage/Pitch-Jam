using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeReference] GameObject text;
    Quaternion rotation;
    Vector3 scale;
    Button button;
    bool animate = false;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        rotation = text.transform.rotation;
        scale = text.transform.localScale;
        button = GetComponent<Button>();
    }

    private void Update()
    {
        text.transform.rotation = rotation;
        text.transform.localScale = scale;
        if (animate)
        {
            time += Time.unscaledDeltaTime;
            text.transform.Rotate(Vector3.back, Mathf.Sin(time));
            float e = (Mathf.Cos(time*0.33f) + 2) * 0.1f;
            text.transform.localScale += new Vector3(e,e,e);
        }
        else {
            time = 0;
            text.transform.rotation = rotation;
            text.transform.localScale = scale;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animate = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animate = false;
    }
}
