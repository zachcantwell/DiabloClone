using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;
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
        [SerializeField] float _experienceReward = 1f;

        void Start()
        {
            _maxHealth = GetStat(Stat.Health);
            GetComponent<Health>().RestoreState((float)_maxHealth);

            _attackPower = GetStat(Stat.AttackPower);
            GetComponent<Fighter>().SetAttackLevelMultiplier(_attackPower);
        }

        public int GetStat(Stat stat)
        {
            return (int)_progression.GetStat(stat, _characterClass, _currentLevel);
        }
    }
}