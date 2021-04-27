using RPG.Combat;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspisionTime = 4f;
        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;


        private Vector3 _guardPos;
        private float _timeSinceLastSawPlayer = 0f;

        private void Start()
        {
            _guardPos = transform.position;
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead())
            {
                return;
            }


            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                _timeSinceLastSawPlayer = 0f;
                AttackBehavior();
            }
            else if (_timeSinceLastSawPlayer < suspisionTime)
            {
                SuspissionBehavior();
            }
            else
            {
                GuardBehavior();
            }

            _timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void SuspissionBehavior()
        {
            fighter.Cancel();
        }

        private void GuardBehavior()
        {
            mover.StartMoveAction(_guardPos);
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}