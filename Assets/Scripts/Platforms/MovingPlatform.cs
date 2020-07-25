using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform {
    [SerializeField] MoveDirection moveDirection;
    [SerializeField] float minMoveSpeed;
    [SerializeField] float maxMoveSpeed;

    [Space, SerializeField] float minPosition;
    [SerializeField] float maxPosition;

    private new Rigidbody2D rigidbody;
    private float moveSpeed;
    private float maxMove;
    private int currentDir;
    private Vector2 startPosition;

    protected override void PlatformStart() {
        base.PlatformStart();

        rigidbody = GetComponent<Rigidbody2D>();
        currentDir = Random.Range(0, 2) == 0 ? -1 : 1;
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        maxMove = Random.Range(minPosition, maxPosition);

        startPosition = new Vector2(transform.position.x, transform.position.y);
    }

    private void Update() {
        if (moveDirection == MoveDirection.Horizontal) {          
            if ((startPosition.x + maxMove) - transform.position.x < 0.1f) {
                currentDir = -1;
            } else if (transform.position.x - (startPosition.x - maxMove) < 0.1f) {
                currentDir = 1;
            }
        } else {
            if ((startPosition.y + maxMove) - transform.position.y < 0.1f) {
                currentDir = -1;
            } else if (transform.position.y - (startPosition.y - maxMove) < 0.1f) {
                currentDir = 1;
            }
        }  
    }

    private void FixedUpdate() {
        if (moveDirection == MoveDirection.Horizontal) {
            rigidbody.velocity = new Vector2(currentDir * moveSpeed, 0f);
        } else {
            rigidbody.velocity = new Vector2(0f, currentDir * moveSpeed);
        }
    }
}

public enum MoveDirection {
    Horizontal,
    Vertical
}