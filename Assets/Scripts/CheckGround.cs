using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {
    private Collider2D collider;
    private PlayerMovement playerMovement;
    public float diePosY;
    public float diePosX;


    private void Start()
    {
        collider = GetComponent<Collider2D>();
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!GameData.isGameOver && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerMovement.isGrounded = true;
            playerMovement.blockJump = false;
            playerMovement.animator.SetInteger("State",  1);
        }
    }
}
