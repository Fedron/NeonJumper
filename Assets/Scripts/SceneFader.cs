using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        SetToCameraTransform();
    }

    private void SetToCameraTransform() {
        Vector3 cameraPos = Camera.main.transform.position;
        transform.position = new Vector3(cameraPos.x, cameraPos.y, 0f);
    }

    public void ChangeScene(int buildIndex) {
        SetToCameraTransform();
        animator.SetTrigger("Fade");
        StartCoroutine(LoadScene(buildIndex));
    }

    IEnumerator LoadScene(int buildIndex) {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(buildIndex);
    }
}
