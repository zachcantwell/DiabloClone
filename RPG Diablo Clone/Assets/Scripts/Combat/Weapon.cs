using RPG.Core;
using UnityEngine;

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

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(_projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, GetWeaponDamage());
        }

        public void SpawnWeapon(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            if (_equippedPrefab)
            {
                Instantiate(_equippedPrefab, GetTransform(rightHandTransform, leftHandTransform));
            }

            if (animator && _animOverride)
            {
                animator.runtimeAnimatorController = _animOverride;
            }
        }
    }
}