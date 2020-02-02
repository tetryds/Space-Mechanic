using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Astronaut
{
    public class Ressuply : MonoBehaviour
    {
        float ressuplyDelay = 60f;
        float timeSinceLastRessuply = 0f;

        [SerializeField] Light lightSpot = null;
        [SerializeField] TargetIndicator targetIndicator;

        private void FixedUpdate()
        {
            timeSinceLastRessuply += Time.fixedDeltaTime;
            if (timeSinceLastRessuply < ressuplyDelay && lightSpot.color != Color.red)
            {
                targetIndicator.SetColor(Color.red);
                lightSpot.color = Color.red;
            }

            if (timeSinceLastRessuply >= ressuplyDelay && lightSpot.color != Color.green)
            {
                targetIndicator.SetColor(Color.green);
                lightSpot.color = Color.green;
            }
        }

        public void RessuplyAstronaut(AstronautPilot astronaut)
        {
            if (timeSinceLastRessuply < ressuplyDelay) return;

            Vector3 dir = transform.position - astronaut.transform.position;
            if (dir.magnitude < 2f)
            {
                astronaut.Ressuply();
                timeSinceLastRessuply = 0f;
            }
        }
    }
}
