using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JsonDataClasses;
using Newtonsoft.Json;

public class ShopManager : MonoBehaviour {

    public static ShopManager Instance { get; private set; }
    public List<ShopJson> shopList = new List<ShopJson>();

    [SerializeField]
    Canvas shopCanvas;
    [SerializeField]
    ShopItem shopItemPrefs;
    [SerializeField]
    GameObject viewPortContent;
    [SerializeField]
    GameObject confirmPanel;
    [SerializeField]
    Button yesButton;
    [SerializeField]
    Button noButton;
    [SerializeField]
    GameObject OnHoverDetailPanel;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemStat;
    [SerializeField]
    TextMeshProUGUI itemDescription;
    [SerializeField]
    TextMeshProUGUI itemPrice;
    [SerializeField]
    TextMeshProUGUI charName;
    [SerializeField]
    TextMeshProUGUI charContext;
    [SerializeField]
    Image charFace;

    private string itemId;
    private ItemJson itemJson;
    private List<ShopItem> shopItems;
    private bool isItemClicked;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Init();
        DontDestroyOnLoad(this);
        isItemClicked = false;
    }

    void Init()
    {
        shopItems = new List<ShopItem>();
        for (int i = 0; i < 30; i++)
        {
            ShopItem temp = Instantiate(shopItemPrefs);
            temp.transform.parent = viewPortContent.transform;
            shopItems.Add(temp);
        }
        var shopsJson = Resources.Load<TextAsset>("JSON/shops");
        ShopJson[] tempShop = JsonConvert.DeserializeObject<ShopJson[]>(shopsJson.text);
        shopList.AddRange(tempShop);
    }

    public void OnShow(string shopid)
    {
        ShopJson shop = FindShopById(shopid);
        if (shop != null)
        {

            charName.text = shop.shopDialog.name;
            charContext.text = shop.shopDialog.context;
            Sprite texture = Resources.Load<Sprite>(shop.shopDialog.imgPath);
            if (texture)
            {
                charFace.sprite = texture;
            }

            string[] items = shop.shopItemIds;
            int i = 0;
            for (; i < items.Length; i++)
            {
                ItemJson item;
                if (items[i].ToLower().Contains("iw"))
                {
                    WeaponJson weapon = DataManager.Instance.SearchWeaponID(items[i]);
                    item = weapon;
                }
                else if (items[i].ToLower().Contains("ia"))
                {
                    ArmorJson armor = DataManager.Instance.SearchArmorID(items[i]);
                    item = armor;
                }
                else
                {
                    item = DataManager.Instance.SearchUsableID(items[i]);
                }
                Sprite sprite = Resources.Load<Sprite>(item.imgPath);
                shopItems[i].gameObject.SetActive(true);
                shopItems[i].OnShow(sprite, item.id);
            }
            for (; i < shopItems.Count; i++)
            {
                shopItems[i].gameObject.SetActive(false);
            }
            shopCanvas.gameObject.SetActive(true);
        }
    }

    public void OnExit()
    {
        confirmPanel.gameObject.SetActive(false);
        OnHoverDetailPanel.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(false);
        isItemClicked = false;
        itemJson = null;
    }

    public void OnHoverItem(string itemid)
    {
        if (!isItemClicked)
        {
            OnHoverDetailPanel.SetActive(true);
            ItemJson item;
            if (itemid.ToLower().Contains("iw"))
            {
                WeaponJson weapon = DataManager.Instance.SearchWeaponID(itemid);
                item = weapon;
                itemStat.text = "Require level: " + item.lvlRequirement + "\n" +
                    "Type: " + item.type.ToString() + "\n" +
                    "PATK: " + weapon.patk + "\n" +
                    "MATK: " + weapon.matk;
            }
            else if (itemid.ToLower().Contains("ia"))
            {
                ArmorJson armor = DataManager.Instance.SearchArmorID(itemid);
                item = armor;
                itemStat.text = "Require level: " + item.lvlRequirement + "\n" +
                    "Type: " + item.type.ToString() + "\n" +
                    "PATK: " + armor.pdef + "\n" +
                    "MATK: " + armor.mdef;
            }
            else
            {
                item = DataManager.Instance.SearchUsableID(itemid);
                itemStat.text = "";
            }
            itemDescription.text = item.tooltips;
            itemName.text = item.name;
            itemPrice.text = item.gold + " Gold";
            itemJson = item;
        }
        itemId = itemid;
    }

    public void OnHoverOut()
    {
        if (!isItemClicked)
        {
            OnHoverDetailPanel.SetActive(false);
            confirmPanel.SetActive(false);
        }
    }

    public void OnClickItem()
    {
        isItemClicked = false;
        OnHoverItem(itemId);
        OnHoverDetailPanel.SetActive(true);
        confirmPanel.SetActive(true);
        isItemClicked = true;
    }

    public void OnYes()
    {
       if (PlayerManager.Instance.Currency >= itemJson.gold)
       {
            PlayerManager.Instance.Currency -= itemJson.gold;
            if (itemJson.id.Contains("iw"))
            {
                PlayerManager.Instance.AddItem("weapon", itemJson.id, 1);
            }
            else if (itemJson.id.Contains("ia"))
            {
                PlayerManager.Instance.AddItem("armor", itemJson.id, 1);
            }
            else
            {
                PlayerManager.Instance.AddItem("usable", itemJson.id, 1);
            }
       }
    }

    public void OnNo()
    {
        confirmPanel.SetActive(false);
        OnHoverDetailPanel.SetActive(false);
        isItemClicked = false;
    }

    ShopJson FindShopById(string id)
    {
        for (int i = 0; i < shopList.Count; i++)
        {
            if (shopList[i].id == id)
            {
                return shopList[i];
            }
        }
        return null;
    }
}
