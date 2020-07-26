using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : Platform {
    [SerializeField] SpriteRenderer stripes;
    [SerializeField] float fallDelay;

    protected override void PlatformStart() {
        base.PlatformStart();
        stripes.size = new Vector2(width, 1f);
    }

    private void Fall() {
        GetComponent<Animator>().SetTrigger("Fall");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Invoke("Fall", fallDelay);
        }
    }
}
