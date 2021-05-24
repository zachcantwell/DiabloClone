using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float currentHealthPoints = 100f;

        bool isDead = false;

        void Start()
        {
            currentHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            currentHealthPoints = Mathf.Max(currentHealthPoints - damage, 0);

            if (currentHealthPoints <= 0)
            {
                Die(instigator);
            }
        }

        public float GetHealthPercentage()
        {
            float maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            float healthPercent = (currentHealthPoints / maxHealth) * 100f;
            healthPercent = Mathf.Round(healthPercent);

            return healthPercent;
        }

        private void Die(GameObject instigator)
        {
            if (isDead) return;

            isDead = true;
            AwardExperience(instigator);
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            if (instigator != null)
            {
                Experience experience = instigator.GetComponent<Experience>();

                if (experience)
                {
                    float xpReward = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
                    instigator.GetComponent<Experience>().GainExperience(xpReward);
                }
            }
        }

        public void RestoreState(object state)
        {
            float health = (float)state;
            currentHealthPoints = health;

            if (currentHealthPoints <= 0)
            {
                Die(null);
            }
        }

        public object CaptureState()
        {
            return currentHealthPoints;
        }


    }
}