using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoGrass : MonoBehaviour
{
    [SerializeReference] public Sprite discoSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("disco") == 1) {
            GetComponent<SpriteRenderer>().sprite = discoSprite;
        }
    }
}
