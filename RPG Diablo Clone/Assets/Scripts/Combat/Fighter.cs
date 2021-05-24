using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] AnimatorOverrideController _weaponOveride;
        [SerializeField] Transform _rightHandTransform = null;
        [SerializeField] Transform _leftHandTransform = null;
        [SerializeField] Weapon _defaultWeapon;
        [SerializeField] private int _attackLevel = 1;
        Weapon _currentWeapon = null;
        Health targetsHealth;
        float timeSinceLastAttack = Mathf.Infinity;


        void Start()
        {
            if (_currentWeapon == null)
            {
                EquipWeapon(_defaultWeapon);
            }
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

            if (_currentWeapon.HasProjectile())
            {
                _currentWeapon.LaunchProjectile(gameObject, _rightHandTransform, _leftHandTransform, targetsHealth);
            }
            else
            {
                Debug.Log("Weapon Damage = " + _currentWeapon.GetWeaponDamage().ToString() + " * attackLevel = " + (_currentWeapon.GetWeaponDamage() * _attackLevel).ToString());
                targetsHealth.TakeDamage(gameObject, _currentWeapon.GetWeaponDamage() * _attackLevel);
            }
        }

        public Health GetTargetsHealth()
        {
            return targetsHealth;
        }

        void Shoot()
        {
            Hit();
        }

        public void SetAttackLevelMultiplier(int level)
        {
            _attackLevel = level;
        }

        private bool GetIsInRange()
        {
            if (targetsHealth && _currentWeapon)
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
            _currentWeapon.SpawnWeapon(_rightHandTransform, _leftHandTransform, GetComponent<Animator>());
        }

        public object CaptureState()
        {
            return _currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}