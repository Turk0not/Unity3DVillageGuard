using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float waypointTolerence = 1f;
        [SerializeField] float waypointLifeTime = 3f;

        [SerializeField] PatrolPath patrolPath;
        
        GameObject player;
        Figther figther;
        Health health;
        Mover mover;

        Vector3 enemyLocation;
        float timeSinceLastSawPlayer;
        float timeSinceArrivedWaypoint;
        int currentWaypointIndex = 0;

        
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
                Vector3 nextPosition = enemyLocation;

                if (patrolPath != null)
                {
                    if (AtWaypoint())
                    {
                        timeSinceArrivedWaypoint = 0;
                        CycleWaypoint();
                    }

                    nextPosition = GetNextWaypoint();
                }

                if (timeSinceArrivedWaypoint > waypointLifeTime)
                {
                    mover.StartMoveAction(nextPosition);
                }
                
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedWaypoint += Time.deltaTime;
        }

        private Vector3 GetNextWaypoint()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceWaypoint = Vector3.Distance(transform.position, GetNextWaypoint());
            return distanceWaypoint < waypointTolerence;
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

