using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyManager : CharacterManager
    {
        EnemyStats enemyStats;
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimatorManager;


        public State currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidbody;

        public bool isInteracting;
        public bool isPreformingAction;
        
        public float maximumAttackRange = 1.5f;
        public float rotationSpeed = 15;


        [Header("A.I setting")]
        public float detectionRadius;
        //The higher and lower respectively these angles are the greater detectopn Field of view ( bascically like eye sight)
        public float minimumDetectionAngle= -50;
        public float maximumDetectionAngle=50;

        public float currentRecoveryTime = 0;

        private void Awake()
        {
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyStats = GetComponent<EnemyStats>();
            enemyRigidbody = GetComponent<Rigidbody>();
      

        }
        private void Start()
        {
            navMeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
        }
        private void Update()
        {
            HandleRecoveryTimer();

            isInteracting = enemyAnimatorManager.animator.GetBool("isInteracting");

        }

        private void FixedUpdate()
        {
            HandleStateMachine();


        }
        private void HandleStateMachine()
        {
          if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);
                if(nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if (isPreformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPreformingAction = false;
                }
            }
        }
        #region Attack


        #endregion
    }

}