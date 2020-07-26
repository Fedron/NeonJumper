using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupShopItemUI : MonoBehaviour {
    public PowerupShopItem item;

    [Space, SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Button buyButton;
    [SerializeField] GameObject disabledPanel;
    [SerializeField] GameObject alreadyBought;

    private void Start() {
        image.sprite = item.sprite;
        nameText.text = item.itemName;
        descText.text = item.itemDesc;
        priceText.text = string.Concat("$", item.price.ToString());

        if (PlayerPrefs.GetInt(string.Concat(item.itemName, "-bought"), 0) == 1) {
            disabledPanel.SetActive(true);
            alreadyBought.SetActive(true);
        } else if (PlayerPrefs.GetInt("money", 0) < item.price) {
            buyButton.interactable = false;
        }
    }

    public void BuyItem(ShopManager sm) {
        bool success = item.BuyItem(sm);
        if (success) {
            disabledPanel.SetActive(true);
            alreadyBought.SetActive(true);
        }
    }
}
