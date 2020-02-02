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

        //private DifficultySettings settings;
        private static int settingId = 0;

        private static readonly DifficultySettings hard = new DifficultySettings(150f, 100f, 0.66f, 30f, 22f, 500f);
        private static readonly DifficultySettings medium = new DifficultySettings(175f, 130f, 0.73f, 30f, 18f, 500f);
        private static readonly DifficultySettings easy = new DifficultySettings(175f, 130f, 0.73f, 30f, 18f, 500f);
        //readonly float maxDuration = 150f;
        //readonly float minDurarion = 100f;
        //readonly float minRepair = 0.66f;
        //readonly float maxTimeToExplode = 30f;
        //readonly float dmgRateWhenBroken = 22f;
        //readonly float explosionDmg = 500f;
        private static readonly DifficultySettings[] settingsList = new DifficultySettings[3]
        {
            new DifficultySettings(150f, 100f, 0.66f, 30f, 22f, 500f),
            new DifficultySettings(175f, 130f, 0.73f, 30f, 18f, 500f),
            new DifficultySettings(240f, 200f, 0.80f, 50f, 12f, 200f)
        };

        float duration;

        StationController station;

        private void Start()
        {
            station = transform.parent.GetComponentInParent<StationController>();
            Fix();
            timeToExplode = settingsList[settingId].maxTimeToExplode;
            materialProperty = new MaterialPropertyBlock();
            matRenderer = GetComponent<Renderer>();

            sliderImage = healthSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
            Debug.Log(duration);
        }

        private void FixedUpdate()
        {
            if (exploded || StationController.Win || !StationController.IsAlive) return;
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
                station.Hit(settingsList[settingId].dmgRateWhenBroken * Time.fixedDeltaTime);
                timeToExplode -= Time.fixedDeltaTime;
                healthSlider.value = (1 - timeToExplode / settingsList[settingId].maxTimeToExplode);
                if (timeToExplode <= 0)
                {
                    Explode();
                }
            }
        }

        private void Explode()
        {
            smoke.Stop();
            station.Hit(settingsList[settingId].explosionDmg);
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

            duration = Random.Range(settingsList[settingId].minDurarion, settingsList[settingId].maxDuration);
            smoke.Stop();
            timeToBreak = Random.Range(settingsList[settingId].minRepair * duration, duration);
            timeToExplode = settingsList[settingId].maxTimeToExplode;
            broken = false;
        }

        public static void IncreaseDifficulty()
        {
            if (settingId > 0)
                settingId--;
        }

        public static void DecreaseDifficulty()
        {
            if (settingId < settingsList.Length - 1)
                settingId++;
        }

        public struct DifficultySettings
        {
            public readonly float maxDuration;
            public readonly float minDurarion;
            public readonly float minRepair;
            public readonly float maxTimeToExplode;
            public readonly float dmgRateWhenBroken;
            public readonly float explosionDmg;

            public DifficultySettings(float maxDuration, float minDurarion, float minRepair, float maxTimeToExplode, float dmgRateWhenBroken, float explosionDmg)
            {
                this.maxDuration = maxDuration;
                this.minDurarion = minDurarion;
                this.minRepair = minRepair;
                this.maxTimeToExplode = maxTimeToExplode;
                this.dmgRateWhenBroken = dmgRateWhenBroken;
                this.explosionDmg = explosionDmg;
            }
        }
    }
}
