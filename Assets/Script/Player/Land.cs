using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            AudioManager.Instance.PlaySFX("Land");
        }
    }

    private void Damage()
    {
        
    }
}
