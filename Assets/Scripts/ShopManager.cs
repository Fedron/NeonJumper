using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour {
    [SerializeField] SceneFader sceneFader;
    [SerializeField] AudioClip gameTheme;
    [SerializeField] TextMeshProUGUI moneyText;

    [Space, SerializeField] Image powerupShopButton;
    [SerializeField] Image skinShopButton;

    [Space, SerializeField] GameObject powerupsShop;
    [SerializeField] GameObject skinsShop;

    private void Start() {
        UpdateMoneyText();
        AudioManager.Instance.PlayMusic(gameTheme);
        powerupShopButton.color = new Color(0, 0, 0, 0.39f);
        skinShopButton.color = new Color(1, 1, 1, 0.39f);
    }

    public void UpdateMoneyText() {
        moneyText.text = string.Concat("$", PlayerPrefs.GetInt("money", 0));
    }

    public void ViewPowerupShop() {
        powerupShopButton.color = new Color(0, 0, 0, 0.39f); ;
        skinShopButton.color = new Color(1, 1, 1, 0.39f);

        powerupsShop.SetActive(true);
        skinsShop.SetActive(false);
    }

    public void ViewSkinsShop() {
        powerupShopButton.color = new Color(1, 1, 1, 0.39f);
        skinShopButton.color = new Color(0, 0, 0, 0.39f); ;

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
        sceneFader.ChangeScene(0);
    }

    public void PlayButton() {
        sceneFader.ChangeScene(1);
    }
}
