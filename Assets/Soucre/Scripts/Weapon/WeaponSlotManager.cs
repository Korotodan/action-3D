using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerManager playerManager;
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;


        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        public WeaponItem attackingWeapon;

        Animator animator;

        QuickSlotUI quickSlotUI;

        PlayerStats playerStats;
        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            animator = GetComponent<Animator>();
            quickSlotUI = FindObjectOfType<QuickSlotUI>();
            playerStats = GetComponentInParent<PlayerStats>();
     

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if(weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if(weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotUI.UpdateWeaponQuickSlotUI(true, weaponItem);
            }
            else
            {
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotUI.UpdateWeaponQuickSlotUI(false, weaponItem);
            }
        }

        #region Handle Weapon's Damage Collider
        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenDamageCollider()
        {
            if (playerManager.isUsingRightHand)
            {
                rightHandDamageCollider.EnableDamageCollier();
            }
            else if(playerManager.isUsingLeftHand)
            {
                leftHandDamageCollider.EnableDamageCollier();
            }
          
        }

        //public void OpenLeftDamageCollider()
        //{
        //    leftHandDamageCollider.EnableDamageCollier();
        //}

        public void CloseDamageCollider()
        {
            if(rightHandDamageCollider != null)
            {
                rightHandDamageCollider.DisableDamageCollier();
            }

            if(leftHandDamageCollider != null)
            {
                leftHandDamageCollider.DisableDamageCollier();
            }
     
        }

        //public void CloseDamageCollider()
        //{
        //    leftHandDamageCollider.DisableDamageCollier();
        //}

        #endregion

        #region Handle Weapon's Stamina Drainage
        public void DrainStaminaLightAttack() {
      
            playerStats.TakeStaminaDamge(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }


        public void DrainStaminaHeavyAttack()
        {
            playerStats.TakeStaminaDamge(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }

        #endregion

    }

}