using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewArea : MonoBehaviour
{
    private Enemy master;
    private Collider2D v_collider;
    [Range(1f,2f)] [SerializeField] private float punchDistance;

    private void Start()
    {
        master = transform.GetComponentInParent<Enemy>();
        v_collider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (master.CanPunch && Vector2.Distance(master.transform.position, collision.gameObject.transform.position) < punchDistance && collision.gameObject.layer == LayerMask.NameToLayer("Player")) master.CloseAttack(collision.gameObject);
        if (master.CountCharges > 0 && collision.gameObject.layer == LayerMask.NameToLayer("Player")) master.Attack();
    }
}
