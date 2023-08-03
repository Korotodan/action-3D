using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerStats : CharacterStats
    {

        PlayerManager playerManager;
        HealthBar healthBar;
        StaminaBar staminaBar;
        AnimatorHandler animatorHandler;
        public float staminaRegenerationAmout;
        public float staminaRegenTimer;
        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            healthBar = FindFirstObjectByType<HealthBar>();
            staminaBar = FindFirstObjectByType<StaminaBar>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            maxStamina = SetMaxHealthFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);

        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = maxHealth * 10;
            return maxHealth;
        }
        private float SetMaxHealthFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeStaminaDamge(int damage)
        {
            currentStamina = currentStamina - damage;

            staminaBar.SetCurrentStamina(currentStamina);

        }

        public void TakeDamage (int damage)
        {
            if (playerManager.isInvulnerable)
                return;

            if (isDead)
                return;
          
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Hit", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                isDead = true;
            }
        }
       
        public void RegenerateStamina()
        {
            if (playerManager.isInteracting)
            {
                staminaRegenTimer = 0;
            }
            if (!playerManager.isInteracting)
            {
                staminaRegenTimer += Time.deltaTime;
                staminaRegenerationAmout = 0.01f * maxStamina;
                if (currentStamina <= maxStamina&& staminaRegenTimer> 1)
                {
                    currentStamina += staminaRegenerationAmout * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }

            }
   
          
        }
    }

}