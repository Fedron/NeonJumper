using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionChanger : MonoBehaviour {
    [SerializeField] GameObject resolution480;
    [SerializeField] GameObject resolution720;

    private void Start() {
        int width = PlayerPrefs.GetInt("resX", 480);
        int height = PlayerPrefs.GetInt("resY", 720);
        Screen.SetResolution(width, height, false);

        if (width == 480) {
            resolution480.SetActive(true);
            resolution720.SetActive(false);
        } else {
            resolution480.SetActive(false);
            resolution720.SetActive(true);
        }
    }

    public void SwitchResolutions() {
        resolution480.SetActive(!resolution480.activeInHierarchy);
        resolution720.SetActive(!resolution720.activeInHierarchy);

        bool res480Active = resolution480.activeInHierarchy ? true : false;
        if (res480Active) {
            Screen.SetResolution(480, 720, false);
            PlayerPrefs.SetInt("resX", 480);
            PlayerPrefs.SetInt("resY", 720);
        } else {
            Screen.SetResolution(720, 1280, false);
            PlayerPrefs.SetInt("resX", 720);
            PlayerPrefs.SetInt("resY", 1280);
        }
    }
}
