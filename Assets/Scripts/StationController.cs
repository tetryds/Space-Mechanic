using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

namespace Astronaut
{
    public class StationController : MonoBehaviour
    {
        [SerializeField] Slider healthSlider;
        Image sliderImage;

        float health;
        readonly float maxHealth = 1000f;

        System.TimeSpan winTime = new System.TimeSpan(0, 5, 0);
        public static bool Win { get; private set; } = false;

        [SerializeField] GameObject winMessage;

        public static bool IsAlive { get; private set; } = true;
        float gameOverDelayTime = 15f;
        [SerializeField] GameObject gameOverMsg;

        [SerializeField] TextMeshProUGUI clock;

        private void Start()
        {
            IsAlive = true;
            Win = false;
            health = maxHealth;
            sliderImage = healthSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
            HandleClock();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                FixPoint.IncreaseDifficulty();
                ReloadScene();
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                FixPoint.DecreaseDifficulty();
                ReloadScene();
            }
        }

        private void FixedUpdate()
        {
            HandleClock();
            HandleResetGame();
            if (Win || !IsAlive) HandleResetGameOver();
            if (Win)
            {
                HandleWin();
                return;
            }
            if (!IsAlive) HandleGameOverMessage();
            if (health < 0 && IsAlive) DestroyStation();

            winTime = winTime.Subtract(System.TimeSpan.FromSeconds(Time.fixedDeltaTime));

            if (winTime.TotalSeconds <= 0)
            {
                Win = true;
            }

            float current = healthSlider.value;
            float target = health / maxHealth;
            healthSlider.value += (target - current) * 2f * Time.fixedDeltaTime;

            float healthRate = health / maxHealth;
            float deathRate = 1 - healthRate;
            Color healthState = new Color(deathRate, healthRate, 0);
            sliderImage.color = healthState;
        }

        private void HandleClock()
        {
            clock.text = winTime.Minutes + ":" + winTime.Seconds.ToString("00");
        }

        private void HandleResetGame()
        {
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Joystick1Button6))
                ReloadScene();
        }

        private void HandleResetGameOver()
        {
            if (Input.anyKey)
                ReloadScene();
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }

        private void HandleWin()
        {
            winMessage.SetActive(true);
        }

        public void Hit(float dmg)
        {
            health -= dmg;
        }

        private void DestroyStation()
        {
            IsAlive = false;

            Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = false;
                rb.AddForceAtPosition(UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(100, 180), rb.transform.position, ForceMode.Impulse);
                rb.transform.Find("Explosion").GetComponent<ParticleSystem>()?.Play();
            }
        }

        private void HandleGameOverMessage()
        {
            gameOverDelayTime -= Time.fixedDeltaTime;
            if (gameOverDelayTime <= 0)
                gameOverMsg.SetActive(true);

        }
    }
}
