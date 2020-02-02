using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Astronaut
{
    public class StationController : MonoBehaviour
    {
        [SerializeField] Slider healthSlider;

        float health;
        readonly float maxHealth = 1000f;

        private void Start()
        {
            health = maxHealth;
        }

        private void FixedUpdate()
        {
            float current = healthSlider.value;
            float target = health / maxHealth;
            healthSlider.value += (target - current) * 2f * Time.fixedDeltaTime;
        }

        public void Hit(float dmg)
        {
            health -= dmg;
        }
    }
}
