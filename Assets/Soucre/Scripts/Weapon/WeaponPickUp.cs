using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            PickUpItem(playerManager);

        }


        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;

            int time = 2;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

            playerLocomotion.rigidbody.velocity = Vector3.zero;
            animatorHandler.PlayTargetAnimation("Pick Up Item", true);
            playerInventory.weaponsInvetory.Add(weapon);
            playerManager.itemInteractalbeGameObject.GetComponentInChildren<Text>().text = weapon.ItemName;
            playerManager.itemInteractalbeGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
            playerManager.itemInteractalbeGameObject.SetActive(true);
            // them thoi gian dong thong bao item
            gameObject.SetActive(false);
            if (time > Time.deltaTime+time)
            {
                playerManager.itemInteractalbeGameObject.SetActive(false);
                Destroy(gameObject);
            }




        }
    }

}