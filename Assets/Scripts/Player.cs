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

    [Header("VFX")]
    [SerializeField] GameObject jumpVFX;
    [SerializeField] GameObject deathVFX;
    [SerializeField] CameraShakeProfile deathShake;

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
        if (GameManager.Instance.gameOver) return;

        if (Input.GetButtonDown("Jump") && isGrounded) {
            canJump = true;
            AudioManager.Instance.PlaySound2D("Jump");
            Destroy(Instantiate(jumpVFX, new Vector3(transform.position.x, transform.position.y - (playerSize.y / 2), transform.position.z), Quaternion.identity), 0.5f);
        }
        moveInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        if (GameManager.Instance.gameOver) return;

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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Death") && !GameManager.Instance.gameOver) {
            CameraShake.ShakeOnce(deathShake);
            AudioManager.Instance.PlaySound2D("Death");
            GameManager.Instance.GameOver();

            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;

            GetComponent<SpriteRenderer>().enabled = false;
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }
    }
}
