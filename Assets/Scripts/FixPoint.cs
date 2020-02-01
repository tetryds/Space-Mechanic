using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astronaut
{
    public class FixPoint : MonoBehaviour
    {
        [SerializeField] Renderer matRenderer = null;
        MaterialPropertyBlock materialProperty;

        bool broken = false;

        float timeToBreak;
        float timeToExplode;

        float maxDuration = 240f;
        float minDuration = 60f;
        float maxTimeToExplode = 60f;

        private void Start()
        {
            Fix();
            timeToExplode = maxTimeToExplode;
            materialProperty = new MaterialPropertyBlock();
        }

        private void FixedUpdate()
        {

            if (!broken)
            {
                timeToBreak -= Time.fixedDeltaTime;
                if (timeToBreak <= 0 && !broken)
                {
                    Break();
                }
                materialProperty.SetColor("_BaseColor", new Color(1 - timeToBreak / maxDuration, timeToBreak / maxDuration, 0));
                matRenderer.SetPropertyBlock(materialProperty);
            }
            else
            {
                timeToExplode -= Time.fixedDeltaTime;
                if (timeToExplode <= 0)
                {
                    Explode();
                }
            }
        }

        private void Explode()
        {
            Debug.Log("Boom");
        }

        private void Break()
        {
            Debug.Log("Broke");
            broken = true;
        }

        public void Fix()
        {
            Debug.Log("Fixed");
            timeToBreak = Random.Range(minDuration, maxDuration);
            timeToExplode = maxTimeToExplode;
            broken = false;
        }
    }
}
