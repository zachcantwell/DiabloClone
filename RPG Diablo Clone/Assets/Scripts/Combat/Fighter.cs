using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] Weapon _defaultWeapon = null;
        [SerializeField] Transform _handTransform = null;
        [SerializeField] AnimatorOverrideController _weaponOveride;
        Health targetsHealth;
        Weapon _currentWeapon;
        float timeSinceLastAttack = Mathf.Infinity;


        void Start()
        {
            EquipWeapon(_defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (targetsHealth == null) return;
            if (targetsHealth.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(targetsHealth.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(targetsHealth.transform);
            if (timeSinceLastAttack > _currentWeapon.GetTimeBetweenAttacks())
            {
                // This will trigger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if (targetsHealth == null)
            {
                return;
            }
            targetsHealth.TakeDamage(_currentWeapon.GetWeaponDamage());
        }

        private bool GetIsInRange()
        {
            if (targetsHealth)
            {
                return Vector3.Distance(transform.position, targetsHealth.transform.position) < _currentWeapon.GetWeaponRange();
            }
            else
            {
                return false;
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetsHealth = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            targetsHealth = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (weapon == null)
            {
                return;
            }
            _currentWeapon = weapon;
            _currentWeapon.SpawnWeapon(_handTransform, GetComponent<Animator>());
        }
    }
}