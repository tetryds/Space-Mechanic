using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Astronaut
{
    public class FixPoint : MonoBehaviour
    {
        [SerializeField] Slider healthSlider;
        Image sliderImage;

        Renderer matRenderer = null;
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
            matRenderer = GetComponent<Renderer>();

            sliderImage = healthSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
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
                float fixRate = timeToBreak / maxDuration;
                float breakRate = 1 - fixRate;
                Color currentState = new Color(breakRate, fixRate, 0);
                materialProperty.SetColor("_BaseColor", currentState);
                matRenderer.SetPropertyBlock(materialProperty);

                healthSlider.value = fixRate;
                sliderImage.color = currentState;
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
