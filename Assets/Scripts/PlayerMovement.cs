using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float jumpPower;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool blockJump = false;
    [HideInInspector] public Animator animator;
    private Rigidbody2D rigidbody;
    private UIController UICtrl;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        UICtrl = FindObjectOfType<UIController>();
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        if (!GameData.isGameOver && !blockJump)
        {
            if(isGrounded)
            {
                SoundController.instance.JupmPlayer();
                animator.SetInteger("State", 2);
                rigidbody.AddForce(Vector3.up * (jumpPower * rigidbody.mass * rigidbody.gravityScale));
                isGrounded = false;
            }
            else
            {
                SoundController.instance.JupmPlayer();
                animator.SetInteger("State", 3);
                blockJump = true;
                rigidbody.velocity = new Vector2(0, 0);
                rigidbody.AddForce(Vector3.up * (jumpPower * rigidbody.mass * rigidbody.gravityScale));
            }  
        }
    }

    public void Die()
    {
        animator.SetInteger("State", 4);
        GameData.isGameOver = true;
        UICtrl.LoseFrame();
    }
}
