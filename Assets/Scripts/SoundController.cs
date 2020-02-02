using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Astronaut
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] AudioSource[] audioSources;

        int current;

        private void Start()
        {
            foreach (AudioSource audio in audioSources)
            {
                audio.Stop();
            }

            current = Random.Range(0, audioSources.Length - 1);
            audioSources[current].Play();
        }

        private void FixedUpdate()
        {
            if (!audioSources[current].isPlaying)
            {
                current = GetNext();
                audioSources[current].Play();
            }

            if (Input.GetKeyDown(KeyCode.U))
                audioSources[current].Stop();
        }

        public int GetNext()
        {
            if (current == audioSources.Length - 1)
                return 0;
            return current + 1;
        }
    }
}
