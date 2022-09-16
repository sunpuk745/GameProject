using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    NewPlayerMovement playerControls;
    PlatformEffector2D platformEffector;
    private Transform player;
    public Rigidbody2D playerRB;

    void Awake()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
            playerControls = collision.gameObject.GetComponent<NewPlayerMovement>();
            player.GetComponent<Rigidbody2D>().gravityScale += 4;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(playerControls == null)
            return;
        if (playerControls.fallThrough)
        {
            platformEffector.rotationalOffset = 180;
            playerControls = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerControls = null;
        player.GetComponent<Rigidbody2D>().gravityScale -= 4;
        platformEffector.rotationalOffset = 0;
    }
}
