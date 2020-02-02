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
            healthSlider.value = health / maxHealth;
        }
    }
}
