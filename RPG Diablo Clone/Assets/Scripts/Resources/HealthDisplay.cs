using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private Text _playerHealthPercentText;
        [SerializeField] private Text _targetHealthPercentText;

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
                Health targetsHealth = _playerHealth.GetComponent<Fighter>().GetTargetsHealth();
                if (targetsHealth)
                {
                    float targetHealthPercent = targetsHealth.GetHealthPercentage();
                    _targetHealthPercentText.text = targetHealthPercent.ToString() + "%";
                }
                else if (_targetHealthPercentText != null)
                {
                    _targetHealthPercentText.text = "";
                }

                float playerHealthPercent = _playerHealth.GetHealthPercentage();
                _playerHealthPercentText.text = playerHealthPercent.ToString() + "%";
            }

        }
    }
}
