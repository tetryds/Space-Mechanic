using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Astronaut
{
    public class StationPart : MonoBehaviour
    {
        [SerializeField] float sensitivity;
        public float Sensitivity { get { return sensitivity; } }

        //StationController station;

        //private void Start()
        //{
        //    station = transform.parent.GetComponent<StationController>();
        //}

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject == gameObject) return;
        //    float dmg = collision.impulse.magnitude;
        //    if (dmg > sensitivity)
        //        station.Hit(dmg * 10 / sensitivity);
        //}
    }
}
