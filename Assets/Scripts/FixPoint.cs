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

        [SerializeField] ParticleSystem smoke;
        [SerializeField] ParticleSystem explosion;

        Renderer matRenderer = null;
        MaterialPropertyBlock materialProperty;

        bool broken = false;
        bool exploded = false;

        float timeToBreak;
        float timeToExplode;

        //Fail fast for testing stats
        //readonly float maxDuration = 30f;
        //readonly float minDuration = 20f;
        //readonly float maxTimeToExplode = 10f;
        //readonly float dmgRateWhenBroken = 30f;
        //readonly float explosionDmg = 500f;

        readonly float maxDuration = 150f;
        readonly float minDurarion = 100f;

        float duration = 150f;
        readonly float minRepair = 0.66f;
        readonly float maxTimeToExplode = 30f;
        readonly float dmgRateWhenBroken = 18f;
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
            if (exploded) return;
            if (!broken)
            {
                timeToBreak -= Time.fixedDeltaTime;
                if (timeToBreak <= 0 && !broken)
                {
                    Break();
                }
                float fixRate = timeToBreak / duration;
                float breakRate = 1 - fixRate;
                Color currentState = new Color(breakRate, fixRate, 0);
                healthSlider.value = fixRate;
                SetColors(currentState);
            }
            else
            {
                station.Hit(dmgRateWhenBroken * Time.fixedDeltaTime);
                timeToExplode -= Time.fixedDeltaTime;
                healthSlider.value = (1 - timeToExplode / maxTimeToExplode);
                if (timeToExplode <= 0)
                {
                    Explode();
                }
            }
        }

        private void Explode()
        {
            smoke.Stop();
            station.Hit(explosionDmg);
            explosion.Play();
            exploded = true;
            SetColors(Color.black);
        }

        private void Break()
        {
            smoke.Play();
            broken = true;
        }

        private void SetColors(Color color)
        {
            materialProperty.SetColor("_BaseColor", color);
            matRenderer.SetPropertyBlock(materialProperty);

            
            sliderImage.color = color;
            targetIndicator.SetColor(color);
        }

        public void Fix()
        {
            if (exploded) return;

            duration = Random.Range(minDurarion, maxDuration);
            smoke.Stop();
            Debug.Log("Fixed");
            timeToBreak = Random.Range(minRepair * duration, duration);
            timeToExplode = maxTimeToExplode;
            broken = false;
        }
    }
}
