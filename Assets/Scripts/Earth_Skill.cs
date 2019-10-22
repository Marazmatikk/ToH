using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Skill : MonoBehaviour
{
    [SerializeField] private GameObject earthUpSound;
    [SerializeField] private GameObject scrollSound;
    [SerializeField] private float speed;
    [SerializeField] private int angle;
    private int count = 0;
    private int countRotate;
    private bool kinematic = true;
    private Vector3 direction;
    private Vector3 localPos;
    private bool moveObject = false;
    private Rigidbody2D rigid2d;

    private void Start()
    {
        countRotate = 80 / -angle;
        rigid2d = GetComponent<Rigidbody2D>();
    }

    private void Update ()
    {
        Traslate_Earth();
    }

    private void Traslate_Earth()
    {
        //Вращение объекта
        if (count != countRotate)
        {
            transform.Rotate(0, 0, angle);
            count++;
            if (count == countRotate)
            {
                Destroy(earthUpSound);
                Instantiate(scrollSound, transform.position, Quaternion.identity, transform);
            }
        }
        //Движение при соприкосновении с игроком
        if (moveObject)
        {
            if (kinematic)
            {
                direction = transform.InverseTransformDirection(Vector3.right);
                transform.Translate(direction * Time.deltaTime * speed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundController.instance.ExplosionEarth();
        if (collision.gameObject.tag == "Flying_Earth")
        {
            rigid2d.bodyType = RigidbodyType2D.Dynamic;
            kinematic = false;
            rigid2d.velocity = new Vector2(0, 0);
        }
        if(collision.gameObject.layer != 9 && collision.gameObject.layer != 8)
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 8)
        {
            moveObject = true;
        }
    }
}
