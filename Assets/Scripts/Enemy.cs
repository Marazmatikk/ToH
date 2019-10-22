using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected internal GameObject element;
    public bool CanPunch { get; private set; } = true;
    public int CountCharges { get; protected set; }
    [SerializeField] private float punchDamage;
    protected internal Animator animator;
    private float timeAttack;
    private Collider2D collider;
    [SerializeField] private float timeAttackKD;
    public enum EnemyType
    {
        Earth = 1,
        Fire
    }
    public EnemyType typeElement;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        CountCharges = (int)typeElement;
        timeAttack = timeAttackKD;
    }

    private void Update()
    {
        AttackTimer();
    }

    public virtual void Attack()
    {
        if(CountCharges>0 && timeAttack < 0)
        {
            SoundController.instance.EnemieFireAttack();
            GameObject elem = null;
            elem = Instantiate(element, transform.position + Vector3.left * 0.8f + Vector3.up, Quaternion.identity, transform);
            elem.GetComponent<Fire>().master = Fire.Master.Enemy;
            animator.SetTrigger("Attack");
            animator.SetTrigger("Idle");
            CountCharges--;
            timeAttack = timeAttackKD;
        }
    }

    public void CloseAttack(GameObject player)
    {
        if (CanPunch)
        {
            collider.enabled = false;
            player.GetComponent<HPScript>().TakeDamage(punchDamage);
            SoundController.instance.EnemyPunch();
            animator.SetTrigger("Attack");
            animator.SetTrigger("Idle");
            CanPunch = false;
            StartCoroutine(EnableCollider());
        }
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1);
        collider.enabled = true;
    }

    private void AttackTimer()
    {
        if(timeAttack>0)
        {
            timeAttack -= Time.deltaTime;
        }
    }
}
