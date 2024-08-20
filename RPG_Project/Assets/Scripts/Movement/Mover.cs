using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();      
            health = GetComponent<Health>();    
        }

        void Update()
        {

            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 hit)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            GetComponent<Figther>().Cancel();
            navMeshAgent.destination = hit;
            navMeshAgent.isStopped = false;
        }


        //Bastýðýn yere karakterin gitmesini saðlayacak metod
        public void MoveTo(Vector3 hit)
        {
            navMeshAgent.destination = hit;
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
