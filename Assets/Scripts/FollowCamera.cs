using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset;

    [Space, SerializeField] Transform background1;
    [SerializeField] Transform background2;
    [SerializeField] float backgroundSize = 20.48f;

    private void Start() {
    }

    private void LateUpdate() {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if (transform.position.y >= background2.position.y) {
            background1.position = new Vector3(background1.position.x, background2.position.y + backgroundSize, background1.position.z);
            SwitchBackgrounds();
        }

        if (transform.position.y < background1.position.y) {
            background2.position = new Vector3(background2.position.x, background1.position.y - backgroundSize, background2.position.z);
            SwitchBackgrounds();
        }
    }

    private void SwitchBackgrounds() {
        Transform temp = background1;
        background1 = background2;
        background2 = temp;
    }
}
