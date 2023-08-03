using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public float detecionRadius = 2;
        public LayerMask detectionLayer;

        //Animation
        public string sleepAnimation;
        public string wakeAnimation;

        public PursueTargetState pursueTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if(isSleeping && enemyManager.isInteracting == false)
            {
                // Animation sleep
                enemyAnimatorManager.PlayTargetAnimation(sleepAnimation, true);
            }
            #region Handle Target Detection 
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detecionRadius, detectionLayer);
            for (int i = 0; i<colliders.Length; i++)
            {
                CharacterStats playerManager = colliders[i].transform.GetComponent<CharacterStats>();
                if( playerManager != null)
                {
                    Vector3 targetDirection = playerManager.transform.position - enemyManager.transform.position;
                    float viewbleAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                    if(viewbleAngle > enemyManager.minimumDetectionAngle && viewbleAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = playerManager;
                        isSleeping = false;
                        enemyAnimatorManager.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }

            #endregion

            #region Handle State Change 

            if(enemyManager.currentTarget != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}
