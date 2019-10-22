using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public enum Master
    {
        Player,
        Enemy
    }
    public Master master = Master.Player;
    [SerializeField] private float speed = 2.5f;
    private int damage;
    private int minDam = 30;
    private int maxDam = 35;

    void Start ()
    {
        if (master == Master.Player) damage = 1;
        else damage = Random.Range(minDam, maxDam);
        if (master == Master.Enemy) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update ()
    {
        Movement();
    }

    private void Movement()
    {
        if (master == Master.Enemy)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<HPScript>().TakeDamage(damage);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<HPScript>().TakeDamage(damage);
        }
        SoundController.instance.ExplosionFire();
        Destroy(gameObject);
    }
}
