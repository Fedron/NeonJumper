using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingPlatform : Platform {
    [SerializeField] float minFlashInterval;
    [SerializeField] float maxFlashInterval;
    [SerializeField] float minIdleTime;
    [SerializeField] float maxIdleTime;

    private Animator animator;
    private float flashInterval;
    private float idleTime;

    protected override void PlatformStart() {
        base.PlatformStart();

        animator = GetComponent<Animator>();
        flashInterval = Random.Range(minFlashInterval, maxFlashInterval);
        idleTime = Random.Range(minIdleTime, maxIdleTime);

        StartCoroutine("Animate");
    }

    private IEnumerator Animate() {
        while (true) {
            yield return new WaitForSeconds(idleTime);
            animator.SetTrigger("out");
            yield return new WaitForSeconds(flashInterval);
            animator.SetTrigger("in");
        }
    }
}
