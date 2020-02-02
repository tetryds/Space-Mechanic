using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Astronaut
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform astronaut = null;

        [SerializeField] float smoothFactor = 10f;

        float mouseSpeedFactor = 0.6f;

        float joystickSpeedFactor = 3f;

        Vector3 mouseStartPos = Vector3.zero;

        Vector3 mouseDelta = Vector3.zero;

        float pitchSpeed = 60f;
        float yawSpeed = 60f;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            mouseDelta += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        //}

        //private void FixedUpdate()
        //{
            transform.position += (astronaut.position - transform.position) * smoothFactor * Time.deltaTime;

            float pitch = Input.GetAxisRaw("LookUp") * joystickSpeedFactor + mouseDelta.y * mouseSpeedFactor;
            float yaw = Input.GetAxisRaw("LookRight") * joystickSpeedFactor + mouseDelta.x * mouseSpeedFactor;


            if (pitch < 0 && Vector3.Dot(Vector3.up, transform.forward) < 0.6f ||
                pitch > 0 && Vector3.Dot(Vector3.up, transform.forward) > -0.6f)
                transform.Rotate(transform.right, pitch * pitchSpeed * Time.deltaTime, Space.World);

            transform.Rotate(Vector3.up, yaw * yawSpeed * Time.deltaTime, Space.World);
            mouseDelta = Vector3.zero;
        }
    }
}
