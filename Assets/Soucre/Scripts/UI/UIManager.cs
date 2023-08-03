using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class UIManager : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        public EquipmentWindowUI equipmentWindowUI;

        [Header("UI Window")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject weaponInventoryWindow;
        public GameObject weaponEquimentScreenWindow;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool rightHandSlot03Selected;
        public bool rightHandSlot04Selected;
        public bool ledftHandSlot01Selected;
        public bool ledftHandSlot02Selected;
        public bool ledftHandSlot03Selected;
        public bool ledftHandSlot04Selected;


        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotParent;
        WeaponInventorySlot[] weaponInventorySlots;

      



        private void Awake()
        {


        }
        private void Start()
        {
            
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
          if(playerInventory != null)
            {
                Debug.Log("Player Inventory"+ playerInventory);
                equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
            }
          else
            {
                Debug.Log("null");
            }
        }
        public void UpdateUI()
        {
            #region Weapon Inventory Slots
            for(int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.weaponsInvetory.Count)
                {
                    if (weaponInventorySlots.Length < playerInventory.weaponsInvetory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInvetory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }



            #endregion
        }
        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }

    
        public void CLoseAllEquipmentWindow()
        {
       
        }
        public void CLoseAllInventoryWindows()
        {
            RestAllSelectedSlots();
            weaponInventoryWindow.SetActive(false);
            weaponEquimentScreenWindow.SetActive(false);
        }

        public void RestAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            rightHandSlot03Selected = false;
            rightHandSlot04Selected = false;
            ledftHandSlot01Selected = false;
            ledftHandSlot02Selected = false;
            ledftHandSlot03Selected = false;
            ledftHandSlot04Selected = false;

        }
    }
}
