using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;

        public WeaponItem unarmeWeapon;

        public WeaponItem[] weaponsInRightHandSlot = new WeaponItem[1];
        public WeaponItem[] WeaponsInLeftHandSlot = new WeaponItem[1];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        public List<WeaponItem> weaponsInvetory;

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();

        }

        private void Start()
        {
            rightWeapon = weaponsInRightHandSlot[0];
            leftWeapon = WeaponsInLeftHandSlot[0];
            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);

        }

        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex = currentRightWeaponIndex + 1;
            if(currentRightWeaponIndex == 0 && weaponsInRightHandSlot[0]!= null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if (currentRightWeaponIndex == 0 && weaponsInRightHandSlot[0] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }


            if (currentRightWeaponIndex == 1 && weaponsInRightHandSlot[1] != null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlot[1] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }
            else if (currentRightWeaponIndex == 2 && weaponsInRightHandSlot[2] != null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if (currentRightWeaponIndex == 2 && weaponsInRightHandSlot[2] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }
            else if (currentRightWeaponIndex == 3 && weaponsInRightHandSlot[3] != null)
            {
                rightWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlot[currentRightWeaponIndex], false);
            }
            else if (currentRightWeaponIndex == 3 && weaponsInRightHandSlot[3] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }

            else
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }

            if(currentRightWeaponIndex > weaponsInRightHandSlot.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmeWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmeWeapon, false);
            }
            Debug.Log("Change RIght Hand Weapon");
        }

        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            if (currentLeftWeaponIndex == 0 && WeaponsInLeftHandSlot[0] != null)
            {
                leftWeapon = WeaponsInLeftHandSlot[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(WeaponsInLeftHandSlot[currentLeftWeaponIndex], true);
               
            }
            else if (currentLeftWeaponIndex == 0 && WeaponsInLeftHandSlot[0] == null)
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }


            if (currentLeftWeaponIndex == 1 && WeaponsInLeftHandSlot[1] != null)
            {
                leftWeapon = WeaponsInLeftHandSlot[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(WeaponsInLeftHandSlot[currentLeftWeaponIndex], true);
               
            } 
            else if (currentLeftWeaponIndex == 2 && WeaponsInLeftHandSlot[2] != null)
            {
                leftWeapon = WeaponsInLeftHandSlot[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(WeaponsInLeftHandSlot[currentLeftWeaponIndex], true);

            }
            else if (currentLeftWeaponIndex == 3 && WeaponsInLeftHandSlot[3] != null)
            {
                leftWeapon = WeaponsInLeftHandSlot[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(WeaponsInLeftHandSlot[currentLeftWeaponIndex], true);

            }
            else
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }

            if (currentLeftWeaponIndex > WeaponsInLeftHandSlot.Length - 1)
            {
               
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmeWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmeWeapon, true);
            }
        
        }
    }

}