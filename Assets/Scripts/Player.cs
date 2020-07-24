using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;

    [Header("Jumping")]
    [SerializeField] float jumpVelocity = 10f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] float groundCheckHeight = 0.05f;
    [SerializeField] LayerMask groundMask;

    private new Rigidbody2D rigidbody;

    private Vector2 playerSize;
    private Vector2 boxSize;
    private bool canJump;
    private bool isGrounded;
    private float moveInput;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundCheckHeight);
    }

    private void Update() {
        if (Input.GetButtonDown("Jump") && isGrounded) canJump = true;
        moveInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        // Jumping Logic
        if (canJump) {
            rigidbody.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            canJump = false;
        } else {
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            isGrounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundMask) != null);
        }

        if (rigidbody.velocity.y < 0) {
            rigidbody.gravityScale = fallMultiplier;
        } else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump")) {
            rigidbody.gravityScale = lowJumpMultiplier;
        } else {
            rigidbody.gravityScale = 1f;
        }

        // Horizontal movement
        rigidbody.velocity = new Vector2(moveInput * moveSpeed, rigidbody.velocity.y);
    }
}
