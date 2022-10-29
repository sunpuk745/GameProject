using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image frontHealthBar;
    public Image backHealthBar;

    [SerializeField] private float LerpSpeedSetting = 1f;
    [SerializeField] private GameManager gameManager;

    float health, maxHealth = 0;
    float LerpSpeed;
    
    private void Update()
    {
        maxHealth = gameManager.playerMaxHP;
        health = gameManager.currentHealth;
        if (health > maxHealth) health = maxHealth;
        LerpSpeed = LerpSpeedSetting * Time.deltaTime;
        HealthBarFiller();
    }

    void HealthBarFiller()
    {
        frontHealthBar.fillAmount = health / maxHealth;
        backHealthBar.fillAmount = Mathf.Lerp(backHealthBar.fillAmount, health/maxHealth, LerpSpeed);
    }
}
