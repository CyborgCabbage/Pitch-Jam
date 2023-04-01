using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AI
{
    public class AIDetector : MonoBehaviour
    {
        [field: SerializeField]
        public bool PlayerDetected { get; private set; }
        public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

        [Header("OverlapCircle Parameters")]
        [SerializeField]
        private Transform detectorOrigin;
        public Vector2 detectorSize = Vector2.one;
        public Vector2 detectorOriginOffset = Vector2.zero;

        public float detectionDelay = 0.3f;

        public LayerMask detectorLayerMask;

        [Header("Gizmo Parameters")]
        public Color gizmoIdleColour = Color.blue;
        public Color gizmoDetectedColour = Color.red;
        public bool showGizmos = true;

        private GameObject target;

        public GameObject Target
        {
            get => target;
            private set
            {
                target = value;
                PlayerDetected = target != null;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DetectionCoroutine());
        }

        IEnumerator DetectionCoroutine()
        {
            yield return new WaitForSeconds(detectionDelay);
            PerformDetection();
            StartCoroutine(DetectionCoroutine());
        }

        public void PerformDetection()
        {
            Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
            if (collider != null)
            {
                Target = collider.gameObject;
            }
            else
            {
                Target = null;
            }
        }

        private void OnDrawGizmos()
        {
            if (showGizmos && detectorOrigin != null)
            {
                Gizmos.color = gizmoIdleColour;
                if (PlayerDetected)
                {
                    Gizmos.color = gizmoDetectedColour;
                }
                Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
            }
        }
    }
}