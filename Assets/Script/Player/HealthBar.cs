using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;
    float health, maxHealth = 100;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health > maxHealth) health = maxHealth;
        HealthBarFiller();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void Damage(float damagePoints)
    {
        if (health > 0){
            health -= damagePoints;
        }
    }

    public void Heal(float damagePoints)
    {
        if (health < maxHealth>){
            health += damagePoints;
        }
    }
}
