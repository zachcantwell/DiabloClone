using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController _animOverride;
        [SerializeField] GameObject _equippedPrefab = null;
        [SerializeField] float weaponRange = Mathf.Epsilon;
        [SerializeField] float weaponDamage = Mathf.Epsilon;
        [SerializeField] float timeBetweenAttacks = Mathf.Epsilon;

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

        public void SpawnWeapon(Transform handTransform, Animator animator)
        {
            if (_equippedPrefab)
            {
                Instantiate(_equippedPrefab, handTransform);
            }

            if (animator && _animOverride)
            {
                animator.runtimeAnimatorController = _animOverride;
            }
        }
    }
}