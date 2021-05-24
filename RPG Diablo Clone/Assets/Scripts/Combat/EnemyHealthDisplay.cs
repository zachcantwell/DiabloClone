using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        private Text _enemyHealthPercentText;
        private Fighter _fighter;
        void Awake()
        {
            _enemyHealthPercentText = GetComponent<Text>();
            _fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }

        void Update()
        {
            if (_fighter == null || _fighter.GetTargetsHealth() == null)
            {
                _enemyHealthPercentText.text = "";
            }
            else if (_fighter)
            {
                Health targetsHealth = _fighter.GetTargetsHealth();
                if (targetsHealth)
                {
                    float targetHealthPercent = targetsHealth.GetHealthPercentage();
                    _enemyHealthPercentText.text = targetHealthPercent.ToString() + "%";
                }
            }
        }
    }
}