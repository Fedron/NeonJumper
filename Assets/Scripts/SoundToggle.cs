using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {
    [SerializeField] Toggle toggle;

    private void Start() {
        if (PlayerPrefs.GetFloat("sfx vol", 1f) == 0f) {
            toggle.isOn = false;
        } else {
            toggle.isOn = true;
        }
    }

    public void ToggleSound() {
        if (toggle.isOn) {
            AudioManager.Instance.SetVolume(1f, AudioManager.AudioChannel.Sfx);
        } else {
            AudioManager.Instance.SetVolume(0f, AudioManager.AudioChannel.Sfx);
        }
    }
}
