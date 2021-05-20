using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 5)] [SerializeField] int _currentLevel = 1;
        [SerializeField] CharacterClass _characterClass;
        [SerializeField] Progression _progression = null;
        [SerializeField] int _maxHealth = 1;
        [SerializeField] int _attackPower = 1;

        void Start()
        {
            _maxHealth = _progression.GetHealthForClassLevel(_characterClass, _currentLevel);
            GetComponent<Health>().RestoreState((float)_maxHealth);

            _attackPower = _progression.GetAttackForClassLevel(_characterClass, _currentLevel);
            GetComponent<Fighter>().SetAttackLevelMultiplier(_attackPower);
        }
    }
}