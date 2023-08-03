using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SG 
{
    public class CameraHandler : MonoBehaviour
    {
        PlayerManager playerManager;
        InputHandler inputHandler;
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        private Vector3 cameraFollowVelocity = Vector3.zero;
        public LayerMask ignoreLayers;
        public LayerMask environmentLayer;
        

        public static CameraHandler singleton ;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        public float targetPosition;
        private float defaulPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35;
        public float maxmumPviot = 35;

        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffSet = 0.2f;
        public float minimumCollisionOffSet = 0.2f;
        public float lockedPivotPostion = 2.25f;
        public float unLockedPivotPosition = 1.65f;


        public Transform currentLockOnTarget;

        List<CharacterManager> availableTargets = new List<CharacterManager>();
        public float maximumLockOnDistance = 30;
        public Transform nearestLockOnTarget;
        public Transform leftLockTarget;
        public Transform rightLockTarget;

        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaulPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
            targetTransform = FindAnyObjectByType<PlayerManager>().transform;
            inputHandler = FindObjectOfType<InputHandler>();
            playerManager = FindObjectOfType<PlayerManager>();
        }

        public void Start()
        {
            environmentLayer = LayerMask.NameToLayer("Environment");
        }

        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);

            myTransform.position = targetPosition;
            HandlerCameraCollision(delta);
        }
        public void HandlerCameraRotation(float delta, float mouseXInput, float mouseInputY)
        {
            if( inputHandler.lockOnFlag == false && currentLockOnTarget == null)
            {
                lookAngle += (mouseXInput * lookSpeed) / delta;
                pivotAngle -= (mouseInputY * pivotSpeed) / delta;
                pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maxmumPviot);

                Vector3 rotation = Vector3.zero;
                rotation.y = lookAngle;
                Quaternion targetRotaion = Quaternion.Euler(rotation);
                myTransform.rotation = targetRotaion;

                rotation = Vector3.zero;
                rotation.x = pivotAngle;

                targetRotaion = Quaternion.Euler(rotation);
                cameraPivotTransform.localRotation = targetRotaion;

            }
            else
            {
                // chua dung toi
                //float velocity = 0;
                Vector3 dir = currentLockOnTarget.position - transform.position;
                dir.Normalize();
                dir.y = 0;

                Quaternion targetRotaion = Quaternion.LookRotation(dir);
                transform.rotation = targetRotaion;

                dir = currentLockOnTarget.position - cameraPivotTransform.position;
                dir.Normalize();

                targetRotaion = Quaternion.LookRotation(dir);
                Vector3 eulerAngle = targetRotaion.eulerAngles;
                eulerAngle.y =0;
                cameraPivotTransform.localEulerAngles = eulerAngle;
            }


        }

        public void HandlerCameraCollision(float delta)
        {
            targetPosition = defaulPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;

            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), ignoreLayers))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);

                targetPosition = -(dis - cameraCollisionOffSet);

            }
            if(Mathf.Abs(targetPosition) < minimumCollisionOffSet)
            {
                targetPosition = -minimumCollisionOffSet;
            }
            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;

        }

        public void HandleLockOn()
        {
            float shortestDistance = Mathf.Infinity;
            float shortestDistanceOfLeftTarget = Mathf.Infinity;
            float shortestDistanceOfRightTarget = Mathf.Infinity;


            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);
            
            for(int i = 0; i<colliders.Length; i++)
            {
                CharacterManager character = colliders[i].GetComponent<CharacterManager>();

                if (character != null)
                {
                    Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position, character.transform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);
                    RaycastHit hit;

                    if(character.transform.root != targetTransform.transform.root 
                        && viewableAngle >-50 && viewableAngle< 50 
                        && distanceFromTarget<= maximumLockOnDistance)
                    {
                        if(Physics.Linecast(playerManager.lockOnTransform.position,character.lockOnTransform.position, out hit))
                        {
                            Debug.DrawLine(playerManager.lockOnTransform.position, character.lockOnTransform.position);

                            if(hit.transform.gameObject.layer == environmentLayer)
                            {
                                //Can
                            }
                            else
                            {
                                availableTargets.Add(character);

                            }
                        }

                      
                    }
                }
            }

            for ( int k =0;k< availableTargets.Count; k++)
            {
                float distanceFormTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);

                if(distanceFormTarget < shortestDistance)
                {
                    shortestDistance = distanceFormTarget;
                    nearestLockOnTarget = availableTargets[k].lockOnTransform;
                }
                if(inputHandler.lockOnFlag)
                {
                    Vector3 relativeEnemyPosition = currentLockOnTarget.InverseTransformPoint(availableTargets[k].transform.position);
                    var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - availableTargets[k].transform.position.x;
                    var distanceFromRightTarget = currentLockOnTarget.transform.position.x + availableTargets[k].transform.position.x;

                    if(relativeEnemyPosition.x> 0.00 && distanceFromLeftTarget< shortestDistanceOfLeftTarget)
                    {
                        shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftLockTarget = availableTargets[k].lockOnTransform;
                    }


                    if(relativeEnemyPosition.x <0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget)
                    {
                        shortestDistanceOfRightTarget = distanceFromRightTarget;
                        rightLockTarget = availableTargets[k].lockOnTransform;
                    }
                }
            }

        }

        public void ClearLockOnTargets()
        {
            availableTargets.Clear();
            nearestLockOnTarget = null;
            currentLockOnTarget = null;
          
        }

        public void SetCameraHieght()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3(0, lockedPivotPostion);
            Vector3 newUnLockedPosition = new Vector3(0, unLockedPivotPosition);

            if(currentLockOnTarget != null)
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
            }
            else
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnLockedPosition, ref velocity, Time.deltaTime);
            }
        }
    }


}
