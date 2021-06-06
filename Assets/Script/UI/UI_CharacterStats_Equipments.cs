using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_CharacterStats_Equipments : MonoBehaviour {

    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI equipmentName;
    [SerializeField]
    private ScrollRect subEquipmentPanel;

    private PlayerCharacter tempChar;
    private PlayerArmor showedArmor;
    private PlayerWeapon showedWeapon;

    // Use this for initialization
    void Start () {
        image = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        equipmentName = transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        subEquipmentPanel = transform.GetChild(1).GetComponent<ScrollRect>();
    }
	
    public void OnShowWeapon(PlayerWeapon weapon)
    {
        subEquipmentPanel.gameObject.SetActive(false);
        if (weapon != null)
        {
            if (weapon.weapon != null)
            {
                image.gameObject.SetActive(true);
                image.sprite = Resources.Load<Sprite>(weapon.weapon.imgPath);
                image.preserveAspect = true;
                equipmentName.text = weapon.weapon.name;
                showedWeapon = weapon;
            }
            else
            {
                image.gameObject.SetActive(false);
                equipmentName.text = "None";
            }
        }
        else
        {
            image.gameObject.SetActive(false);
            equipmentName.text = "None";
        }
    }

    public void OnShowArmor(PlayerArmor armor)
    {
        subEquipmentPanel.gameObject.SetActive(false);
        if (armor != null)
        {
            if (armor.armor != null)
            {
                image.gameObject.SetActive(true);
                image.sprite = Resources.Load<Sprite>(armor.armor.imgPath);
                image.preserveAspect = true;
                equipmentName.text = armor.armor.name;
                showedArmor = armor;
            }
            else
            {
                image.gameObject.SetActive(false);
                equipmentName.text = "None";
            }
        }
        else
        {
            image.gameObject.SetActive(false);
            equipmentName.text = "None";
        }
    }

    public void SetTempChar(PlayerCharacter temp)
    {
        tempChar = temp;
    }

    public void OnButtonClick()
    {
		if (subEquipmentPanel.gameObject.activeSelf)
        {
            subEquipmentPanel.gameObject.SetActive(false);
        }
        else
        {
            subEquipmentPanel.gameObject.SetActive(true);
        }
    }

    public void OnSubItemClick()
    {

    }
}
