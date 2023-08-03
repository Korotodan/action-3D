using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStats
    { 
      
        public HealthBar healthBar;

        AnimatorHandler animatorHandler;


        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            //healthBar.SetMaxHealth(maxHealth);
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = maxHealth * 10;
            return maxHealth;
        }
        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            //healthBar.SetCurrentHealth(currentHealth);

            //animatorHandler.PlayTargetAnimation("Hit", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //animatorHandler.PlayTargetAnimation("Death", true);

            }
        }
    }
}
