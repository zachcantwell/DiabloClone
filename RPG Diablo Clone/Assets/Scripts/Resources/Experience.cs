using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private float _experiencePoints = 0f;

        public void GainExperience(float xp)
        {
            if (xp > 0)
            {
                _experiencePoints += xp;
            }
        }

    }
}