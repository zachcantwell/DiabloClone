using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] _progressionCharacterClass;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] public CharacterClass _characterClass;
            [SerializeField] public int[] _healthLevel;
            [SerializeField] public int[] _attackLevel;
        }

        public int GetHealthForClassLevel(CharacterClass currentClass, int currentLevel)
        {
            foreach (var character in _progressionCharacterClass)
            {
                if (currentClass == character._characterClass)
                {
                    return character._healthLevel[currentLevel - 1];
                }
            }
            return 0;
        }

        public int GetAttackForClassLevel(CharacterClass currentClass, int currentLevel)
        {
            foreach (var character in _progressionCharacterClass)
            {
                if (currentClass == character._characterClass)
                {
                    return character._attackLevel[currentLevel - 1];
                }
            }
            return 0;
        }
    }
}