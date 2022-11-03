using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    [SerializeField]private Enemy2 enemy;
    [SerializeField]private LevelLoader levelLoader;

    float health, maxHealth;
    float lerpSpeed;

    private void Start() 
    {
        maxHealth = 120f;
        health = maxHealth;
    }

    private void Update() 
    {
        lerpSpeed = 2f * Time.deltaTime;

        HealthBarFiller();
        if (health <= 0)
        {
            levelLoader.LoadNextLevel();
        }
    }

    public void HealthBarFiller()
    {
        health = enemy.currentHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health/maxHealth , lerpSpeed);
    }
}
