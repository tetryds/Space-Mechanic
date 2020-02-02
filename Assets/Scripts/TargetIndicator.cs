using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace Astronaut
{
    public class TargetIndicator : MonoBehaviour
    {
        [SerializeField] Transform target;

        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Image targetImage;

        [SerializeField] Camera cam;

        private void Update()
        {
            float dot = Vector3.Dot(cam.transform.forward, target.transform.position - cam.transform.position);
            if (dot > 0)
            {
                Vector3 pos = cam.WorldToScreenPoint(target.position);
                transform.position = pos;
            }
        }

        public void SetColor(Color color)
        {
            text.color = color;
            targetImage.color = color;
        }
    }
}
