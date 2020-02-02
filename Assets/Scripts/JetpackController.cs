using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Astronaut
{
    public class JetpackController : MonoBehaviour
    {
        [SerializeField] ParticleSystem lowBackLeft;
        [SerializeField] ParticleSystem lowBackRight;

        [SerializeField] ParticleSystem highBackLeft;
        [SerializeField] ParticleSystem highBackRight;

        [SerializeField] ParticleSystem lowLeft;
        [SerializeField] ParticleSystem lowRight;

        [SerializeField] ParticleSystem topLeft;
        [SerializeField] ParticleSystem topRight;

        [SerializeField] ParticleSystem sideLeft;
        [SerializeField] ParticleSystem sideRight;

        public void Forward(float value)
        {
            //Debug.Log(value);
            //EmissionModule em;
            //MinMaxCurve rot;

            //em = lowBackLeft.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = lowBackRight.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = highBackLeft.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = highBackRight.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;
            //em = sideLeft.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * -value;
            //em.rateOverTime = rot;

            //em = sideRight.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * -value;
            //em.rateOverTime = rot;
        }

        public void Right(float value)
        {
        }

        public void TurnRight(float value)
        {
            //EmissionModule em;
            //MinMaxCurve rot;

            //em = lowBackLeft.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = highBackLeft.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = sideRight.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = lowBackRight.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = highBackRight.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = sideLeft.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;
        }

        public void Up(float value)
        {
            //EmissionModule em;
            //MinMaxCurve rot;
            //em = lowLeft.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = lowRight.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * value;
            //em.rateOverTime = rot;

            //em = topLeft.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * -value;
            //em.rateOverTime = rot;

            //em = topRight.emission;
            //rot = em.rateOverTime;
            //rot.constant = 20f * -value;
            //em.rateOverTime = rot;
        }
    }
}
