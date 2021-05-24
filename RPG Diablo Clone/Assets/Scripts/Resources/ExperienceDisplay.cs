using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class ExperienceDisplay : MonoBehaviour
    {
        private Text _playerXPText;
        private Health _playerHealth;

        void Awake()
        {
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            _playerXPText = GetComponent<Text>();
        }

        void Update()
        {
            if (_playerHealth)
            {
                float xp = (float)_playerHealth.GetComponent<Experience>().CaptureState();
                _playerXPText.text = xp.ToString();
            }
        }
    }
}
