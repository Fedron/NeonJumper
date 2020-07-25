using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    [SerializeField] float startDelay;
    [SerializeField] float speedIncrease;

    private new Rigidbody2D rigidbody;
    bool canMove;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        Invoke("StartMoving", startDelay);
    }

    private void FixedUpdate() {
        if (!canMove) return;
        else if (GameManager.Instance.gameOver) {
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;
            return;
        }

        rigidbody.velocity = new Vector2(0f, Time.time * speedIncrease);
    }

    private void StartMoving() {
        canMove = true;
    }
}
