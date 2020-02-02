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

        [SerializeField] TargetIndicator targetIndicator;

        Renderer matRenderer = null;
        MaterialPropertyBlock materialProperty;

        bool broken = false;

        float timeToBreak;
        float timeToExplode;

        readonly float maxDuration = 120f;
        readonly float minDuration = 80f;
        readonly float maxTimeToExplode = 60f;
        readonly float dmgRateWhenBroken = 30f;
        readonly float explosionDmg = 500f;

        StationController station;

        private void Start()
        {
            station = transform.parent.GetComponent<StationController>();
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
                targetIndicator.SetColor(currentState);
            }
            else
            {
                station.Hit(dmgRateWhenBroken * Time.fixedDeltaTime);
                timeToExplode -= Time.fixedDeltaTime;
                if (timeToExplode <= 0)
                {
                    Explode();
                }
            }
        }

        private void Explode()
        {
            station.Hit(explosionDmg);
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
