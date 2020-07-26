using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour {
    [SerializeField] Toggle toggle;

    private void Start() {
        if (PlayerPrefs.GetFloat("music vol") == 0f) {
            toggle.isOn = true;
        } else {
            toggle.isOn = false;
        }
    }

    public void ToggleMusic() {
        if (toggle.isOn) {
            AudioManager.Instance.SetVolume(0f, AudioManager.AudioChannel.Music);
        } else {
            AudioManager.Instance.SetVolume(1f, AudioManager.AudioChannel.Music);
        }
    }
}
