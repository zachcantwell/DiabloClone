using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Health _target;
        [SerializeField] private float _projectileSpeed = 2f;
        [SerializeField] private float _damage = 1f;

        void Update()
        {
            if (_target != null)
            {
                transform.LookAt(GetAimLocation(), Vector3.up);
                transform.Translate(transform.forward * _projectileSpeed * Time.deltaTime, Space.World);
            }
        }

        public void SetTarget(Health target, float damage)
        {
            _target = target;
            _damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            if (_target != null)
            {
                CapsuleCollider collider = _target.GetComponent<CapsuleCollider>();

                if (collider)
                {
                    return collider.bounds.center;
                }
                else
                {
                    return _target.transform.position;
                }
            }
            return Vector3.zero;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform == _target.transform)
            {
                Health targetHealth = other.GetComponent<Health>();

                if (targetHealth)
                {
                    targetHealth.TakeDamage(_damage);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}