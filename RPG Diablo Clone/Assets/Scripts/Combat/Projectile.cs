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
        [SerializeField] private bool _canPursueTarget = false;
        [SerializeField] private Vector3? _targetPos = null;
        [SerializeField] private float _timeUntilDestroy = 10f;
        private float _timeSinceSpawn = 0f;

        void Start()
        {
            if (_target)
            {
                transform.LookAt(GetAimLocation(), Vector3.up);
            }
        }

        void Update()
        {
            MoveProjectile();
            SelfDestructionTimer();
        }

        private void MoveProjectile()
        {
            if (_target != null)
            {
                Vector3 targetPosition = GetAimLocation();
                if (_canPursueTarget && _target.IsDead() == false)
                {
                    transform.LookAt(targetPosition, Vector3.up);
                }
                transform.Translate(transform.forward * _projectileSpeed * Time.deltaTime, Space.World);
            }
        }

        private void SelfDestructionTimer()
        {
            _timeSinceSpawn += Time.deltaTime;
            if (_timeSinceSpawn > _timeUntilDestroy)
            {
                Destroy(this.gameObject);
            }
        }

        public void SetTarget(Health target, float damage)
        {
            _target = target;
            _damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            if (_target == null)
            {
                return Vector3.zero;
            }

            CapsuleCollider collider = _target.GetComponent<CapsuleCollider>();
            if (collider == null)
            {
                return _target.transform.position;
            }

            return _target.transform.position + Vector3.up * collider.height / 2;
        }

        void OnTriggerEnter(Collider other)
        {
            if (_target == null)
            {
                return;
            }

            if (other.transform == _target.transform)
            {
                Health targetHealth = other.GetComponent<Health>();

                if (targetHealth)
                {
                    if (targetHealth.IsDead())
                    {
                        return;
                    }
                    targetHealth.TakeDamage(_damage);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}