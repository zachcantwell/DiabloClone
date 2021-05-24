using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        private Text _playerHealthPercentText;
        private Health _playerHealth;

        void Awake()
        {
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            _playerHealthPercentText = GetComponent<Text>();
        }

        void Update()
        {
            if (_playerHealth)
            {
                float playerHealthPercent = _playerHealth.GetHealthPercentage();
                _playerHealthPercentText.text = playerHealthPercent.ToString() + "%";
            }
        }
    }
}
