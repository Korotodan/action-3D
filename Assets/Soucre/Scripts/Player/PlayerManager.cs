using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG { 
    public class PlayerManager : CharacterManager 
    { 
        InputHandler inputHandler;
        Animator animator;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        CharacterStats characterStats;
        PlayerStats playerStats;

        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractalbeGameObject;

        public bool isInteracting;


        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;
        public bool isInvulnerable;
        public bool isDeath;

        private void Awake()
        {
            cameraHandler = FindAnyObjectByType<CameraHandler>();
        }
        void Start()
        {
            playerStats = GetComponent<PlayerStats>();
            characterStats = GetComponent<CharacterStats>();
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            interactableUI = FindFirstObjectByType<InteractableUI>();
        }

        void Update()
        {

            float delta = Time.deltaTime;
     
            isInteracting = animator.GetBool("isInteracting");
            canDoCombo = animator.GetBool("canDoCombo");
            animator.SetBool("isInAir", isInAir);
            isUsingRightHand = animator.GetBool("isUsingRightHand");
            isUsingLeftHand = animator.GetBool("isUsingLeftHand");
            isInvulnerable = animator.GetBool("isInvulnerable");

            inputHandler.TickInput(delta);
            playerLocomotion.HandlerRollingAndSprinting(delta);
            playerLocomotion.HanleJumping(delta);

            if (!isInteracting)
            {
                playerStats.RegenerateStamina();
            }

            CheckForInteractableObject();
        }
        private void FixedUpdate()
        {

         

            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandlerMovement(delta);
            playerLocomotion.HanlderFalling(delta, playerLocomotion.moveDirection);
       
           
        }



        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_Input = false;
            inputHandler.jump_Input = false;
            inputHandler.inventory_Input = false;

            float delta = Time.fixedDeltaTime;
            if (cameraHandler != null)
            {

                cameraHandler.FollowTarget(delta);
                cameraHandler.HandlerCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);

            }


            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }
       

        public void CheckForInteractableObject()
        {
            RaycastHit hit;
            if(Physics.SphereCast(transform.position, 0.3f,transform.forward, out hit,1f, cameraHandler.ignoreLayers))
            {
                if(hit.collider.tag== "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);


                        if (inputHandler.a_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if(interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }
                if(itemInteractalbeGameObject!= null && inputHandler.a_Input)
                {
                    itemInteractalbeGameObject.SetActive(false);
                }
            }
        }
    }
}