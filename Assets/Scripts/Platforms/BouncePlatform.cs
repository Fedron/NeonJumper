using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : Platform {
    [SerializeField] GameObject bounceVFX;
    [SerializeField] float minBounceForce;
    [SerializeField] float maxBounceForce;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(minBounceForce, maxBounceForce), ForceMode2D.Impulse);
            Destroy(Instantiate(bounceVFX, new Vector3(collision.transform.position.x, collision.transform.position.y - 0.5f, 0f), Quaternion.identity), 0.5f);
            AudioManager.Instance.PlaySound2D("Bounce");
        }
    }
}
