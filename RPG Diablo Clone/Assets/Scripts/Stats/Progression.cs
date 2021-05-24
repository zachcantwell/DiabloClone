using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] _progressionCharacterClass = null;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] public CharacterClass _characterClass;
            [SerializeField] public ProgressionStat[] _progressionStat;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat _stat;
            public int[] _levels;
        }

        public float GetStat(Stat stat, CharacterClass currentClass, int currentLevel)
        {
            foreach (ProgressionCharacterClass progressionClass in _progressionCharacterClass)
            {
                if (currentClass != progressionClass._characterClass)
                {
                    continue;
                }

                foreach (ProgressionStat progressionStat in progressionClass._progressionStat)
                {
                    if (progressionStat._stat != stat || progressionStat._levels.Length < currentLevel)
                    {
                        continue;
                    }
                    return progressionStat._levels[currentLevel - 1];
                }
            }
            return 0f;
        }


    }
}