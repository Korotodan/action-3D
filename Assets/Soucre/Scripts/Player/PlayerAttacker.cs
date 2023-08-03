using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            inputHandler = GetComponent<InputHandler>();
        }
         public void HanldeWeaponCombo(WeaponItem weaponItem)
         {
            if (inputHandler.comboFlag)
            {
                animatorHandler.animator.SetBool("canDoCombo", false);
                if (lastAttack == weaponItem.OH_Light_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weaponItem.OH_Light_Attack_2, true);
                }
            }
            
               
         }
 
        public void HandlerLightAttack(WeaponItem weaponItem)
        {
            weaponSlotManager.attackingWeapon = weaponItem;
            animatorHandler.PlayTargetAnimation(weaponItem.OH_Light_Attack_1, true);
            lastAttack = weaponItem.OH_Light_Attack_1;
        }
        public void HanlerHeavyAttack(WeaponItem weaponItem)
        {
            
            weaponSlotManager.attackingWeapon = weaponItem;
            animatorHandler.PlayTargetAnimation(weaponItem.OH_Heavy_Attack_1, true);
            lastAttack = weaponItem.OH_Heavy_Attack_1;
        }
    }
}