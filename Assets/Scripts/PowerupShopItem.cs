using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup", menuName = "Shop/Powerup")]
public class PowerupShopItem : ScriptableObject {
    public string itemName;
    public string itemDesc;
    public Sprite sprite;
    public int price;

    public bool BuyItem(ShopManager sm) {
        int playerMoney = PlayerPrefs.GetInt("money");
        if (playerMoney < price) return false;

        PlayerPrefs.SetInt("money", playerMoney - price);
        sm.UpdateMoneyText();

        PlayerPrefs.SetInt(string.Concat(itemName, "-bought"), 1);

        return true;
    }
}
