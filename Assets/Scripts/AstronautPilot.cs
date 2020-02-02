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

        [SerializeField] Toggle isDamping;

        [SerializeField] Transform cameraTransform;

        Vector3 prevRotation = Vector3.zero;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = new AstronautAnimController(GetComponentInChildren<Animator>());
        }

        void Update()
        {
            Debug.DrawRay(transform.position, rb.angularVelocity);
            //camAngleAdjust = Vector3.SignedAngle(camera.forward, transform.forward, transform.right);
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
                TryFixNearby();

            if (Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                dampen = !dampen;
                isDamping.isOn = dampen;
            }
        }

        private void FixedUpdate()
        {
            //transform.eulerAngles = cameraTransform.eulerAngles;
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

            //Yaw
            //Roll

            //rb.AddRelativeTorque(pitch.eulerAngles * torquePower * Time.fixedDeltaTime);

            Vector3 headingError = Vector3.Cross(transform.forward, cameraTransform.forward);
            rb.AddTorque(headingError * torquePower * Time.deltaTime);
            //transform.LookAt(transform.position + cameraTransform.forward * 10);

            //if (dampen)
            //    rb.AddForce(-rb.velocity * translationDamp * Time.fixedDeltaTime);
            translation.Normalize();
            rb.AddForce(translation * power * Time.fixedDeltaTime);

            Vector3 animDir = transform.InverseTransformDirection(translation);
            //anim.Forward(translation.z);
            //anim.Right(-translation.x);
            anim.Up(animDir.y);
            //anim.TurnRight(rotation.y);

            //prevRotation = rotation;
        }

        private void TryFixNearby()
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
            }
        }
    }
}
