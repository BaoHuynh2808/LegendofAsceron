using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour {

    [SerializeField]
    InventoryUI_Items inventoryItem;
    [SerializeField]
    ScrollRect scrollView;

    List<InventoryUI_Items> itemList;

	// Use this for initialization
	void Awake () {
        itemList = new List<InventoryUI_Items>();
        for (int i = 0; i < 20; i++)
        {
            InventoryUI_Items temp = Instantiate<InventoryUI_Items>(inventoryItem);
            temp.transform.parent = scrollView.content;
            itemList.Add(temp);
        }
        //gameObject.SetActive(false);
	}

    public void OnButtonClick()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            OnShowInventory();
        }
    }

    public void OnShowInventory()
    {
        scrollView.verticalScrollbar.value = 1;
        int i = 0;
        for (int j=0; j<PlayerManager.Instance.Usables.Count;)
        {
            PlayerUsable temp = PlayerManager.Instance.Usables[j];
            itemList[i].OnShow(temp.usable.id, "usable", temp.amount);
            j++;
            i++;
        }
        for (int j = 0; j < PlayerManager.Instance.Weapons.Count;)
        {
            PlayerWeapon temp = PlayerManager.Instance.Weapons[j];
            itemList[i].OnShow(temp.weapon.id, "weapon", 0);
            j++;
            i++;
        }
        for (int j = 0; j < PlayerManager.Instance.Armors.Count;)
        {
            PlayerArmor temp = PlayerManager.Instance.Armors[j];
            itemList[i].OnShow(temp.armor.id, "armor", 0);
            j++;
            i++;
        }
        for(;i<itemList.Count;i++)
        {
            itemList[i].OnShow(null, "null", 0);
        }
    }
}
