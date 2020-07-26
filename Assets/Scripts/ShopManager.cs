using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour {
    [SerializeField] AudioClip gameTheme;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Button initialButton;

    [Space, SerializeField] GameObject powerupsShop;
    [SerializeField] GameObject skinsShop;

    private void Start() {
        UpdateMoneyText();
        AudioManager.Instance.PlayMusic(gameTheme);
        initialButton.Select();
    }

    public void UpdateMoneyText() {
        moneyText.text = string.Concat("$", PlayerPrefs.GetInt("money", 0));
    }

    public void ViewPowerupShop() {
        powerupsShop.SetActive(true);
        skinsShop.SetActive(false);
    }

    public void ViewSkinsShop() {
        powerupsShop.SetActive(false);
        skinsShop.SetActive(true);
    }

    public void BuyPowerup(PowerupShopItemUI powerup) {
        powerup.BuyItem(this);
    }

    public void BuySkin(SkinShopItemUI skin) {
        skin.BuyItem(this);
    }

    public void EquipSkin(SkinShopItem skin) {
        PlayerPrefs.SetInt("activeSkin", skin.skinID);
        SkinShopItemUI[] skins = FindObjectsOfType<SkinShopItemUI>();
        foreach (SkinShopItemUI s in skins) {
            s.UpdateEquippedStatus();
        }
    }

    public void MenuButton() {
        SceneManager.LoadScene(0);
    }

    public void PlayButton() {
        SceneManager.LoadScene(1);
    }
}
