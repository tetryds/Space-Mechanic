using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Astronaut
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class AstronautController : MonoBehaviour
    {
        Vector3 translation = Vector3.zero;
        Vector3 rotation = Vector2.zero;

        Rigidbody rb;

        [SerializeField] float power = 40f;
        [SerializeField] float torquePower = 10f;

        [SerializeField] float translationDamp = 20f;
        [SerializeField] float angularDamp = 5f;

        [SerializeField] LayerMask fixPointLayer;

        bool dampen = false;

        AstronautAnimController anim;

        [SerializeField] Toggle isDamping;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = new AstronautAnimController(GetComponent<Animator>());
        }

        void Update()
        {
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
            translation.x = Input.GetAxisRaw("Horizontal");
            translation.y = Input.GetAxisRaw("UpDown");
            translation.z = Input.GetAxisRaw("Vertical");
            rotation.x = Input.GetAxisRaw("LookUp");
            rotation.y = Input.GetAxisRaw("LookRight");

            if (dampen)
                rb.AddTorque(-rb.angularVelocity * angularDamp * Time.deltaTime);
            rb.AddRelativeTorque(rotation * torquePower * Time.fixedDeltaTime);

            if (dampen)
                rb.AddForce(-rb.velocity * translationDamp * Time.fixedDeltaTime);
            rb.AddRelativeForce(translation * power * Time.fixedDeltaTime);

            anim.Forward(translation.z);
            anim.Right(-translation.x);
            anim.Up(translation.y);
            anim.TurnRight(rotation.y);
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
