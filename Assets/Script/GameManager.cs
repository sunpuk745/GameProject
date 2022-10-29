using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float playerMaxHP = 100f;
    public float currentHealth;


     void Awake() 
    {
        var gameManagerNum = FindObjectsOfType<GameManager>().Length;
        if (gameManagerNum > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = playerMaxHP;
    }

    public void DecreaseHP(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0f)
        {
            //TODO: Die
        }
        
        //Debug.Log(currentHealth);
    }

}
