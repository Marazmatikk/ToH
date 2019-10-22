using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : MonoBehaviour
{
    [SerializeField] private float damage;
    private Animator animator;
    private Collider2D m_collider;

    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<HPScript>().TakeDamage(damage);
            m_collider.enabled = false;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Element"))
        {
            animator.SetTrigger("Death");
            StartCoroutine(DestroyPhoenix(0.7f));
        }
    }

    IEnumerator DestroyPhoenix(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
