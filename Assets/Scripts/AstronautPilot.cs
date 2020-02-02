using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Astronaut
{
    [RequireComponent(typeof(Rigidbody))]
    public class AstronautPilot : MonoBehaviour
    {
        Vector3 translation = Vector3.zero;
        Vector3 rotation = Vector2.zero;

        Rigidbody rb;

        [SerializeField] float power = 40f;
        [SerializeField] float torquePower = 10f;

        [SerializeField] float translationDamp = 20f;
        [SerializeField] float angularDamp = 5f;

        [SerializeField] float rotDerivativeGain = 0.8f;

        [SerializeField] LayerMask fixPointLayer;

        bool dampen = false;

        AstronautAnimController anim;

        [SerializeField] Transform cameraTransform;

        Vector3 prevRotation = Vector3.zero;


        //Health and status
        [SerializeField] Slider oxygenSlider;
        Image oxygenSliderImg;
        [SerializeField] Slider fuelSlider;
        Image fuelSliderImg;

        private readonly float maxFuel = 100f;
        private readonly float maxOxygen = 100f;

        float fuel = 100f;
        float oxygen = 100f;

        float oxygenConsumption = 0.6f;

        float torqueFuelConsumption = 0.07f;
        float translationFuelConsumption = 0.013f;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = new AstronautAnimController(GetComponentInChildren<Animator>());

            oxygenSliderImg = oxygenSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
            fuelSliderImg = fuelSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();

            PaintSliders();
        }

        void Update()
        {
            if (oxygen <= 0) return;

            if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                TryInteractNearby();
        }

        private void FixedUpdate()
        {
            PaintSliders();
            if (oxygen <= 0) return;

            Vector3 input = Vector3.zero;
            Vector3 translation = Vector3.zero;

            //Vertical
            input.y = Input.GetAxisRaw("UpDown");
            translation.y = input.y;

            //Horizontal
            input.x = Input.GetAxisRaw("Horizontal");
            Vector3 horizontalDir = cameraTransform.TransformDirection(Vector3.right);
            translation += horizontalDir * input.x;

            //Vertical
            input.z = Input.GetAxisRaw("Vertical");
            Vector3 verticalDir = cameraTransform.TransformDirection(Vector3.forward);
            translation += verticalDir * input.z;

            //Pitch
            Quaternion pitch = Quaternion.FromToRotation(transform.forward, cameraTransform.forward);

            //Roll correction
            Vector3 headingError = Vector3.Cross(transform.forward, cameraTransform.forward) + Vector3.Cross(transform.up, Vector3.up);
            Vector3 finalTorque = (headingError * torquePower - rb.angularVelocity * angularDamp) * Time.fixedDeltaTime;

            translation.Normalize();
            Vector3 finalTranslation = translation * power * Time.fixedDeltaTime;

            //Apply thrusters
            if (fuel > 0)
            {
                rb.AddTorque(finalTorque);
                rb.AddForce(finalTranslation);

                Vector3 animDir = transform.InverseTransformDirection(translation);
                anim.Forward(animDir.z);
                anim.Right(-animDir.x);
                anim.Up(animDir.y);
                float rightRot = Vector3.Dot(transform.up, rb.angularVelocity);
                anim.TurnRight(rightRot);
                fuel -= (finalTorque.magnitude * torqueFuelConsumption + finalTranslation.magnitude * translationFuelConsumption) * Time.fixedDeltaTime;
            }

            oxygen -= oxygenConsumption * Time.fixedDeltaTime;

            oxygenSlider.value = oxygen / maxOxygen;
            fuelSlider.value = fuel / maxFuel;
        }

        private void PaintSliders()
        {
            float oxygenRate = oxygen / maxOxygen;
            float noOxygenRate = 1 - oxygenRate;
            Color oxygenState = new Color(noOxygenRate, oxygenRate, 0);
            oxygenSliderImg.color = oxygenState;

            float fuelRate = fuel / maxFuel;
            float noFuelRate = 1 - fuelRate;
            Color fuelState = new Color(noFuelRate, fuelRate, 0);
            fuelSliderImg.color = fuelState;
        }

        private void TryInteractNearby()
        {
            Debug.Log("Trying to fix nearby");
            Collider[] fixPointCols = Physics.OverlapSphere(transform.position, 2f, fixPointLayer.value);

            foreach (Collider fixPointCol in fixPointCols)
            {
                FixPoint fixPoint = fixPointCol.GetComponent<FixPoint>();
                if (fixPoint != null)
                {
                    fixPoint.Fix();
                    Debug.Log("Fixed nearby");
                }
                else
                {
                    Ressuply ressuply = fixPointCol.GetComponent<Ressuply>();
                    if (ressuply != null)
                    {
                        ressuply.RessuplyAstronaut(this);
                    }
                }
            }
        }

        public void Ressuply()
        {
            fuel = maxFuel;
            oxygen = maxOxygen;
        }

    }
}
