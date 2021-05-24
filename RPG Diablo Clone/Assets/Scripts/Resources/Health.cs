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
            currentHealthPoints = GetComponent<BaseStats>().GetMaxHealth();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Max(currentHealthPoints - damage, 0);

            if (currentHealthPoints == 0)
            {
                Die();
            }
        }

        public float GetHealthPercentage()
        {
            float maxHealth = GetComponent<BaseStats>().GetMaxHealth();
            float healthPercent = (currentHealthPoints / maxHealth) * 100f;
            healthPercent = Mathf.Round(healthPercent);

            return healthPercent;
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void RestoreState(object state)
        {
            float health = (float)state;
            currentHealthPoints = health;

            if (currentHealthPoints <= 0)
            {
                Die();
            }
        }

        public object CaptureState()
        {
            return currentHealthPoints;
        }


    }
}