using UnityEngine;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool a_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool jump_Input;
        public bool inventory_Input;
        public bool lockOn_Input;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;


        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;


        public float rollInputTimer;
        public bool rollFlag;
        public bool sprintFlag;
        public bool inventoryFlag;
        public bool comboFlag;
        public bool lockOnFlag;
 


        PlayerController inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        WeaponSlotManager weaponSlotManager;
        AnimatorHandler animatorHandler;
        CameraHandler cameraHandler;
        UIManager uIManager;

        Vector2 movementInput;
        Vector2 cameraInput;


        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            uIManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            //weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }


        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerController();

                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;

                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;

                inputActions.PlayerActions.RB.performed += i => rb_Input = true;
                inputActions.PlayerActions.RT.performed += i => rt_Input = true;
                inputActions.PlayerActions.A.performed += i => a_Input = true;
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
                inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
                inputActions.PlayerActions.LockOnTarget.performed += i => lockOn_Input = true;
    
            }
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void TickInput(float delta)
        {
            HandleMoveInput(delta);
            HandlerRollInput(delta);
            HandleAttackInput(delta);
            HandleQucikSlotInput();
            HandleInventory();
            HandleOnLockInput();
        }

        private void HandleMoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
           
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;

        }

        private void HandlerRollInput(float delta)
        {
            // unity <2021 dung
            //b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            // tu ban unity 2021 tro di dung
            b_Input = inputActions.PlayerActions.Roll.IsPressed();
            sprintFlag = b_Input;
            if (b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;

            }
            else
            {
                if(rollInputTimer>0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            

            
            if (rb_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HanldeWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;

                    animatorHandler.animator.SetBool("isUsingRightHand",true);
                    playerAttacker.HandlerLightAttack(playerInventory.rightWeapon);
                }
  
            }
            if (rt_Input)
            {
                playerAttacker.HanlerHeavyAttack(playerInventory.rightWeapon);
            }
        }

        private void HandleQucikSlotInput()
        {
          
            if (d_Pad_Right)
            {
               
                playerInventory.ChangeRightWeapon();
            }
            else if (d_Pad_Left)
            {
            
                playerInventory.ChangeLeftWeapon();
            }
           
        }

        private void HandleInventory()
        {
            if (inventory_Input)
            {
                inventoryFlag = !inventoryFlag;
                if (inventoryFlag)
                {
                    uIManager.OpenSelectWindow();
                    uIManager.UpdateUI();
                    uIManager.hudWindow.SetActive(false);
                }
                else
                {
                    uIManager.CloseSelectWindow();
                    uIManager.CLoseAllInventoryWindows();
                    uIManager.hudWindow.SetActive(true);
                }
            }
        }

        private void HandleOnLockInput()
        {
            if(lockOn_Input && lockOnFlag == false)
            {
                cameraHandler.ClearLockOnTargets();
                lockOn_Input = false;
               
                cameraHandler.HandleLockOn();
                if(cameraHandler.nearestLockOnTarget != null)
                {
              
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if ( lockOn_Input && lockOnFlag)
            {
                lockOn_Input = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();

            }

            if(lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }


            if(lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.rightLockTarget!= null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }
            cameraHandler.SetCameraHieght();
        }
    }
}
