using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Character_Skill : MonoBehaviour {

    [SerializeField]
    private SkillPanelItem skillItemPref;
    [SerializeField]
    GameObject content;
    [SerializeField]
    TextMeshProUGUI skillPointsText;
    private List<SkillPanelItem> skillItemList;
    private string[] playerSkillArray;
    private string charId;

    private void Awake()
    {
        skillItemList = new List<SkillPanelItem>();
        for (int i=0; i<15; i++)
        {
            SkillPanelItem temp = Instantiate(skillItemPref);
            temp.transform.parent = content.transform;
            temp.parent = this;
            skillItemList.Add(temp);
        }
        gameObject.SetActive(false);
    }

    public void OnShow(string charid)
    {
        if (!gameObject.activeSelf)
        {
            OnUpdate(charid);
        }
        else
        {
            Hide();
        }
    }

    public void OnUpdate(string charid)
    {
        PlayerCharacter playerCharacter = PlayerManager.Instance.SearchCharacterById(charid);
        playerSkillArray = playerCharacter.info.totalSkills;
        charId = charid;
        int i = 0;
        for (; i < playerSkillArray.Length; i++)
        {
            skillItemList[i].OnShow(playerSkillArray[i], playerCharacter.availableSkillPoints, charid);
            skillItemList[i].gameObject.SetActive(true);
        }
        for (; i < skillItemList.Count; i++)
        {
            skillItemList[i].gameObject.SetActive(false);
        }
        skillPointsText.text = "Skill Points: " + playerCharacter.availableSkillPoints;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnClickItem(string skillid)
    {
        int pos = PlayerManager.Instance.ReturnCharacterPosById(charId);
        PlayerManager.Instance.Characters[pos].AddSkill(skillid);
        skillPointsText.text = "Skill Points: " + PlayerManager.Instance.Characters[pos].availableSkillPoints;
        OnUpdate(charId);
    }
}
