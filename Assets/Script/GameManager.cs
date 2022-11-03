using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float playerMaxHP = 100f;
    public float currentHealth;

    public bool isDead;

    [SerializeField] private GameObject healthBar;


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
    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            healthBar.SetActive(false);
        }
        if (scene.buildIndex == 1)
        {
            healthBar.SetActive(false);
        }

        else
        {
            healthBar.SetActive(true);
        }
    }

    public void DecreaseHP(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0f)
        {
            isDead = true;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentHealth = playerMaxHP;
        isDead = false;
    }
}
