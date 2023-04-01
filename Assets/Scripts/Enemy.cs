using SVS.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float time = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).GetComponent<AIDetector>().PlayerDetected)
        {
            StartCoroutine(MoveCoroutine());
        }
        if (transform.GetChild(1).GetComponent<AiMeleeDetector>().PlayerDetected)
        {

            Vector2 force = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            PlayerMovement player = transform.GetChild(0).GetComponent<AIDetector>().Target.GetComponent<PlayerMovement>();
            if(time <= 0)
            {
                player.Roll(force, 50);
                time = 1;
            }
        }
        else
        {
            StopAllCoroutines();
        }
        time -= Time.deltaTime;
    }

    IEnumerator MoveCoroutine()
    {
        Vector2 target = transform.GetChild(0).GetComponent<AIDetector>().Target.transform.position;
        while (Vector2.Distance(transform.position, target) > 0)
        {
            transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
    }


}
