using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private Transform target;
        [SerializeField] private float _maxNavSpeed = 5.6f;

        private NavMeshAgent navMeshAgent;
        private Health health;


        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead())
            {
                navMeshAgent.enabled = false;
            }

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedModifier)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedModifier);
        }

        public void MoveTo(Vector3 destination, float speedModifier)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = _maxNavSpeed * Mathf.Clamp01(speedModifier);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}