using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpotLight : MonoBehaviour
{
    [SerializeField] public float sweepAngle;
    [SerializeField] public float sweepTime;
    Quaternion originalRotation;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        Light2D light = GetComponent<Light2D>();
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        List<Vector2> points = new();
        points.Add(new Vector2(0, 0));
        float angle = Mathf.Deg2Rad*light.pointLightOuterAngle*0.5f;
        float radius = light.pointLightOuterRadius;
        int segments = (int)(light.pointLightOuterAngle / 12.0f + 2.0f);
        for (int i = 0; i <= segments; i++) {
            float a = Mathf.Lerp(angle, -angle, i/(float)segments);
            points.Add(new Vector2(Mathf.Sin(a), Mathf.Cos(a)) * radius);
        }
        collider.points = points.ToArray();
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.rotation = originalRotation;
        float angle = Mathf.Sin(time / sweepTime)*sweepAngle*0.5f;
        transform.Rotate(Vector3.back, angle);
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
