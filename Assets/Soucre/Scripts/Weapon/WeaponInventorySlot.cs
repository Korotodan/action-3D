using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        public Image icon;
        WeaponItem item;
        WeaponSlotManager weaponSlotManager;
        PlayerInventory playerInventory;
        UIManager uiManager;


        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(WeaponItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }
        public void EquipThisItem()
        {
            if (uiManager.rightHandSlot01Selected)
            {
               
                playerInventory.weaponsInvetory.Add(playerInventory.weaponsInRightHandSlot[0]);
                playerInventory.weaponsInRightHandSlot[0] = item;
                playerInventory.weaponsInvetory.Remove(item);

            }
            else if(uiManager.rightHandSlot02Selected)
            {
                
                playerInventory.weaponsInvetory.Add(playerInventory.weaponsInRightHandSlot[1]);
                playerInventory.weaponsInRightHandSlot[1] = item;
                playerInventory.weaponsInvetory.Remove(item);

            }
            else if (uiManager.rightHandSlot03Selected)
            {
                playerInventory.weaponsInvetory.Add(playerInventory.weaponsInRightHandSlot[2]);
                playerInventory.weaponsInRightHandSlot[2] = item;
                playerInventory.weaponsInvetory.Remove(item);
                

            }
            else if (uiManager.rightHandSlot04Selected)
            {
                playerInventory.weaponsInvetory.Add(playerInventory.weaponsInRightHandSlot[3]);
                playerInventory.weaponsInRightHandSlot[3] = item;
                playerInventory.weaponsInvetory.Remove(item);
   

            }

            else if (uiManager.ledftHandSlot01Selected)
            {
                playerInventory.WeaponsInLeftHandSlot[0] = null;
                playerInventory.weaponsInvetory.Add(playerInventory.WeaponsInLeftHandSlot[0]);
                playerInventory.WeaponsInLeftHandSlot[0] = item;
                playerInventory.weaponsInvetory.Remove(item);
   
            }
            else if (uiManager.ledftHandSlot02Selected)
            {
                playerInventory.weaponsInvetory.Add(playerInventory.WeaponsInLeftHandSlot[1]);
                playerInventory.WeaponsInLeftHandSlot[1] = item;
                playerInventory.weaponsInvetory.Remove(item);
     
            }
            else if (uiManager.ledftHandSlot03Selected)
            {
                playerInventory.weaponsInvetory.Add(playerInventory.WeaponsInLeftHandSlot[2]);
                playerInventory.WeaponsInLeftHandSlot[2] = item;
                playerInventory.weaponsInvetory.Remove(item);


            }
            else if (uiManager.ledftHandSlot04Selected)
            {
                playerInventory.weaponsInvetory.Add(playerInventory.WeaponsInLeftHandSlot[3]);
                playerInventory.WeaponsInLeftHandSlot[3] = item;
                playerInventory.weaponsInvetory.Remove(item);
        

            }
            else
            {
                return;
            }
            Debug.Log("Left"+playerInventory.currentLeftWeaponIndex);
            Debug.Log("Right" + playerInventory.currentRightWeaponIndex);

            //playerInventory.rightWeapon = playerInventory.weaponsInRightHandSlot[playerInventory.currentRightWeaponIndex];
            //playerInventory.leftWeapon = playerInventory.WeaponsInLeftHandSlot[playerInventory.currentLeftWeaponIndex];

            weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);

            uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
            uiManager.RestAllSelectedSlots();
        }
       
    }

}