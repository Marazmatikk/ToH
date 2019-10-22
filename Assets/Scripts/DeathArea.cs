using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<HPScript>().TakeDamage(100);
            Destroy(collision.gameObject.GetComponent<PlayerMovement>());
        }
        Destroy(collision.gameObject);
    }
}
