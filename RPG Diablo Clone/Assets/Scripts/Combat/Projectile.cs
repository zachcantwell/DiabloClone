using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Resources;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Health _target;
        [SerializeField] private GameObject[] _destroyOnHit;
        [SerializeField] private GameObject _instigator = null;
        [SerializeField] private float _projectileSpeed = 2f;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private bool _canPursueTarget = false;
        [SerializeField] private Vector3? _targetPos = null;
        [SerializeField] private float _timeUntilDestroy = 0f;
        [SerializeField] ParticleSystem _hitParticles = null;

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

        public void SetTarget(GameObject instigator, Health target, float damage)
        {
            _instigator = instigator;
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
                    _projectileSpeed = 0f;

                    if (targetHealth.IsDead())
                    {
                        return;
                    }

                    targetHealth.TakeDamage(_instigator, _damage);

                    if (_hitParticles)
                    {
                        ParticleSystem particles = Instantiate(_hitParticles, transform.position, Quaternion.identity);
                        particles.Play();
                    }

                    foreach (GameObject gobj in _destroyOnHit)
                    {
                        Destroy(gobj);
                    }

                    ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
                    if (particleSystem)
                    {
                        particleSystem.transform.SetParent(null);
                    }

                    Destroy(this.gameObject, _timeUntilDestroy);
                }
            }
        }
    }
}