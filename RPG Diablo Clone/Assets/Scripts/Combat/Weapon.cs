using RPG.Core;
using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController _animOverride;
        [SerializeField] GameObject _equippedPrefab = null;
        [SerializeField] Projectile _projectile = null;
        [SerializeField] float weaponRange = Mathf.Epsilon;
        [SerializeField] float weaponDamage = Mathf.Epsilon;
        [SerializeField] float timeBetweenAttacks = Mathf.Epsilon;
        [SerializeField] bool _isRightHanded = true;
        const string _weaponName = "Weapon";

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public float GetTimeBetweenAttacks()
        {
            return timeBetweenAttacks;
        }

        public void SetAttackMultiplier(int multiplier)
        {
            weaponDamage *= multiplier;
        }

        public Transform GetTransform(Transform right, Transform left)
        {
            Transform hand = null;
            if (_isRightHanded)
            {
                hand = right;
            }
            else
            {
                hand = left;
            }
            return hand;
        }

        public bool HasProjectile()
        {
            return _projectile != null;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(_weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(_weaponName);
            }
            if (oldWeapon == null)
            {
                return;
            }

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public void LaunchProjectile(GameObject instigator, Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(_projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(instigator, target, GetWeaponDamage());
        }

        public void SpawnWeapon(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);

            if (_equippedPrefab)
            {
                GameObject newWeapon = Instantiate(_equippedPrefab, GetTransform(rightHandTransform, leftHandTransform));
                newWeapon.name = _weaponName;
            }

            var overrider = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animator != null)
            {
                animator.runtimeAnimatorController = _animOverride;
            }
            else if (overrider)
            {
                animator.runtimeAnimatorController = overrider.runtimeAnimatorController;
            }
        }
    }
}