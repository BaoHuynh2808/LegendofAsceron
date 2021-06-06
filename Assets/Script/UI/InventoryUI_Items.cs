using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JsonDataClasses;

public class InventoryUI_Items : MonoBehaviour {
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI quantity;
	
    public void OnShow(string id, string type, int amount)
    {
        gameObject.SetActive(true);
        if (type.ToLower() != "null")
        {
            string path;
            string tempName;
            if (type.ToLower() == "usable")
            {
                UsableJson usable = DataManager.Instance.SearchUsableID(id);
                path = usable.imgPath;
                tempName = usable.name;
                quantity.gameObject.SetActive(true);
                quantity.text = "x" + amount;
            }
            else if (type.ToLower() == "weapon")
            {
                WeaponJson weapon = DataManager.Instance.SearchWeaponID(id);
                path = weapon.imgPath;
                tempName = weapon.name;
                quantity.gameObject.SetActive(false);
            }
            else
            {
                ArmorJson armor = DataManager.Instance.SearchArmorID(id);
                path = armor.imgPath;
                tempName = armor.name;
                quantity.gameObject.SetActive(false);
            }
            image.sprite = Resources.Load<Sprite>(path);
            itemName.text = tempName;
        }
        else
        {
            image.color = new Color(255,255,255,0);
            itemName.text = "";
            quantity.gameObject.SetActive(false);
        }
    }
}
