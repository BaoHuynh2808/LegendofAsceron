using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    Button button;
    [SerializeField]
    Image image;

    string itemId;

    private void Awake()
    {
        button.onClick.AddListener(ShopManager.Instance.OnClickItem);
    }

    public void OnShow(Sprite img, string id)
    {
        image.sprite = img;
        itemId = id;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered");
        ShopManager.Instance.OnHoverItem(itemId);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShopManager.Instance.OnHoverOut();
    }
}
