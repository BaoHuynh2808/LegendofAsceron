using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_CharacterStats_Equipments_SubItem : MonoBehaviour, ISelectHandler, IDeselectHandler {

    [SerializeField]
    UI_CharacterStats_Equipments parent;
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI text;

    private PlayerArmor showedArmor;
    private PlayerWeapon showedWeapon;

    // Use this for initialization
    void Start () {
        image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        text = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void OnSelect(BaseEventData eventData)
    {
    }

    public void OnDeselect(BaseEventData eventData)
    {
    }
	
}
