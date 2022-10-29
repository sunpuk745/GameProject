using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSpirte : MonoBehaviour
{
    private float activeTime = 0.1f;
    private float timeActived;
    private float alpha;
    private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.85f;

    private Transform player;

    private SpriteRenderer sprite;
    private SpriteRenderer playerSprite;

    private Color color;

    private void OnEnable() 
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        sprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActived = Time.time;
    }

    private void Update() 
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        sprite.color = color;

        if(Time.time >= (timeActived + activeTime))
        {
            PlayerAfterImagePool.Instance.AddtoPool(gameObject);
        }
    }
}
