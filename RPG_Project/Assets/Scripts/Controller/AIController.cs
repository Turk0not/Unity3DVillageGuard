using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        
        GameObject player;
        Figther figther;
        Health health;
        Mover mover;

        Vector3 enemyLocation;
        float timeSinceLastSawPlayer;

        
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            figther = GetComponent<Figther>();
            health = GetComponent<Health>();   
            mover = GetComponent<Mover>();

            enemyLocation = transform.position;
        }

        void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if(DistanceToPlayer() < chaseDistance && figther.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                figther.Attack(player);
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                GetComponent<ActionSchedular>().CancelCurrentAction();
            }
            else
            {
                mover.StartMoveAction(enemyLocation);
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

