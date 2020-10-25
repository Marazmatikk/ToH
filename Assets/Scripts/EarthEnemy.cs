using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEnemy : Enemy
{
    public override void Attack()
    {
        if (CountCharges > 0)
        {
            Instantiate(element, transform.position + Vector3.left + Vector3.forward * 0.1f, Quaternion.identity, transform);
            animator.SetTrigger("Raise");
            animator.SetTrigger("Attack");
            CountCharges--;
        }
    }
}
