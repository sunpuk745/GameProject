using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth2 : MonoBehaviour
{
    public Image healthBar;
    [SerializeField]private Enemy3 enemy;

    float health, maxHealth;
    float lerpSpeed;

    private void Start() 
    {
        maxHealth = 200f;
        health = maxHealth;
    }

    private void Update() 
    {
        lerpSpeed = 2f * Time.deltaTime;

        HealthBarFiller();
    }

    public void HealthBarFiller()
    {
        health = enemy.currentHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health/maxHealth , lerpSpeed);
    }
}
