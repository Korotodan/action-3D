using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HandEquipmentSlotUI : MonoBehaviour
    {
        UIManager uIManager;
        public Image icon;
        [SerializeField]
        WeaponItem weapon;

        public bool rightHandSlot01;
        public bool rightHandSlot02;
        public bool rightHandSlot03;
        public bool rightHandSlot04;
        public bool leftHandSlot01;
        public bool leftHandSlot02;
        public bool leftHandSlot03;
        public bool leftHandSlot04;


        public void Awake()
        {
            uIManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(WeaponItem newWeapon)
        {
            weapon = newWeapon;
            icon.sprite = weapon.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            if (rightHandSlot01)
            {
                uIManager.rightHandSlot01Selected = true;

            }else if (rightHandSlot02)
            {
                uIManager.rightHandSlot02Selected = true;
            }
            else if (rightHandSlot03)
            {
                uIManager.rightHandSlot03Selected = true;
            }
            else if (rightHandSlot04)
            {
                uIManager.rightHandSlot04Selected = true;
            }
            else if (leftHandSlot01)
            {
                uIManager.ledftHandSlot01Selected = true;

            }else if (leftHandSlot02)
            {
                uIManager.ledftHandSlot02Selected = true;

            }
            else if(leftHandSlot03)
            {
                uIManager.ledftHandSlot03Selected = true;

            }
            else
            {
                uIManager.ledftHandSlot04Selected = true;

            }

        }
    }

}