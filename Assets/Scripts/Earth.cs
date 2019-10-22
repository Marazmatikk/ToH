using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour {
    [SerializeField] private GameObject earthUpSound;
    [SerializeField] private float speedUp = 2f;
    [SerializeField] private float speed = 6f;
    private int damage = 0;
    private int HP = 2;
    private int minDam = 30;
    private int maxDam = 45;
    private bool height = false;
    private Vector3 pos;
    private Collider2D collider;

    void Start ()
    {
        pos = transform.position + Vector3.up * 1.1f;
        damage = Random.Range(minDam, maxDam);
        collider = GetComponent<Collider2D>();
    }

    void Update ()
    {
        Movement();
    }

    private void Movement()
    {
        if (HP > 0)
        {
            if (!height)
            {
                MoveUp();
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void MoveUp()
    {
        if(transform.position.y < pos.y)
        {
            transform.Translate(Vector3.up * speedUp * Time.deltaTime);
        }
        else
        {
            collider.enabled = true;
            height = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundController.instance.ExplosionEarth();
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<HPScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Element"))
        {
            HP--;
        }
    }
}
