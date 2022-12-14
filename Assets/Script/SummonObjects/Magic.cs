using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float startXPos;
    private float startYPos;
    private float damage = 20f;
    [SerializeField] private float magicDamageRadius;

    private bool isCasted;
    private bool isMagicFinished;

    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private Transform damagePos;

    private Animator anim;
    public float magicVolume = 0.2f;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        isMagicFinished = false;

        FireMagic(damage);

        startXPos = transform.position.x;
        startYPos = transform.position.y;
    }

    private void FixedUpdate() 
    {
        if (isMagicFinished)
        {
            Destroy(gameObject);
        }
    }

    public void TriggerAttack()
    {
        Collider2D[] magicHit = Physics2D.OverlapCircleAll(damagePos.position, magicDamageRadius, whatIsPlayer);
        foreach (Collider2D collider in magicHit)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }

    public void FireMagic(float damage)
    {
        attackDetails.damageAmount = damage;
    }

    public void FinishAttack()
    {
        isMagicFinished = true;
    }

    public void PlayIceExplosionSound()
    {
        AudioManager.Instance.PlaySFX("IceExplosion", (magicVolume));
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(damagePos.position, magicDamageRadius);
    }

}
