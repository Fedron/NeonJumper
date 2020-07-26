using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinShopItemUI : MonoBehaviour {
    public SkinShopItem item;

    [Space, SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Button buyButton;
    [SerializeField] Button equipButton;

    private void Start() {
        image.sprite = item.sprite;
        nameText.text = item.itemName;
        descText.text = item.itemDesc;
        priceText.text = string.Concat("$", item.price.ToString());

        if (PlayerPrefs.GetInt(string.Concat(item.itemName, "-bought"), 0) == 1) {
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);

            int activeSkin = PlayerPrefs.GetInt("activeSkin", 0);
            if (activeSkin == item.skinID) {
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
                equipButton.interactable = false;
            }
        } else if (PlayerPrefs.GetInt("money", 0) < item.price) {
            buyButton.interactable = false;
        }
    }

    public void UpdateEquippedStatus() {
        int activeSkin = PlayerPrefs.GetInt("activeSkin", 0);
        if (activeSkin == item.skinID) {
            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
            equipButton.interactable = false;
        } else {
            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
            equipButton.interactable = true;
        }
    }

    public void BuyItem(ShopManager sm) {
        bool success = item.BuyItem(sm);
        if (success) {
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
        }
    }
}
