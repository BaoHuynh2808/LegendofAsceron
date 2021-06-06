using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CharacterStats : MonoBehaviour {

    #region Info
    [SerializeField]
    private Image face;
    [SerializeField]
    private TextMeshProUGUI charName;
    [SerializeField]
    private Slider hp;
    [SerializeField]
    private Slider mp;
    [SerializeField]
    private Slider exp;
    [SerializeField]
    private TextMeshProUGUI level;
    [SerializeField]
    private TextMeshProUGUI toNextLevel;
    #endregion

    #region Attributes
    [SerializeField]
    private UI_CharacterStats_Attributes str;
    [SerializeField]
    private UI_CharacterStats_Attributes dex;
    [SerializeField]
    private UI_CharacterStats_Attributes intel;
    [SerializeField]
    private UI_CharacterStats_Attributes vit;
    [SerializeField]
    private UI_CharacterStats_Attributes end;
    [SerializeField]
    private UI_CharacterStats_Attributes wis;
    [SerializeField]
    private TextMeshProUGUI availableText;
    [SerializeField]
    private TextMeshProUGUI points;
    [SerializeField]
    private Button confirm;
    #endregion

    #region Equipments
    [SerializeField]
    private UI_CharacterStats_Equipments weapon;
    [SerializeField]
    private UI_CharacterStats_Equipments[] armor;
    #endregion

    #region Stats
    [SerializeField]
    private UI_CharacterStats_Stats[] detailedStats;
    [SerializeField]
    private TextMeshProUGUI battlePointText;
    [SerializeField]
    UI_Character_Skill skillPanel;
    #endregion


    string charId;
    PlayerCharacter character;
    PlayerCharacter tempChar;
    int attributePoints;

    // Use this for initialization
    void Start() {
        charId = string.Empty;
        character = null;
        tempChar = null;
        #region AttributesInit
        attributePoints = 0;

        str.subtractButton.onClick.AddListener(() => str.OnButtonClick(ref attributePoints, false));
        str.addButton.onClick.AddListener(() => str.OnButtonClick(ref attributePoints, true));

        dex.subtractButton.onClick.AddListener(() => dex.OnButtonClick(ref attributePoints, false));
        dex.addButton.onClick.AddListener(() => dex.OnButtonClick(ref attributePoints, true));

        intel.subtractButton.onClick.AddListener(() => intel.OnButtonClick(ref attributePoints, false));
        intel.addButton.onClick.AddListener(() => intel.OnButtonClick(ref attributePoints, true));

        vit.subtractButton.onClick.AddListener(() => vit.OnButtonClick(ref attributePoints, false));
        vit.addButton.onClick.AddListener(() => vit.OnButtonClick(ref attributePoints, true));

        end.subtractButton.onClick.AddListener(() => end.OnButtonClick(ref attributePoints, false));
        end.addButton.onClick.AddListener(() => end.OnButtonClick(ref attributePoints, true));

        wis.subtractButton.onClick.AddListener(() => wis.OnButtonClick(ref attributePoints, false));
        wis.addButton.onClick.AddListener(() => wis.OnButtonClick(ref attributePoints, true));
        #endregion
        gameObject.SetActive(false);
    }

    public void Check(string id)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Unhide(id);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Unhide(string id)
    {
        charId = id;
        SetCharacter(PlayerManager.Instance.SearchCharacterById(id));
        tempChar = new PlayerCharacter(character);
        if (character != null)
        {
            attributePoints = character.availableAttributes;
            ShowInfo();
            ShowAttributes();
            ShowEquipments();
            ShowDetailedStats();
            OnShowSkill();
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Character not found!");
        }
    }

    void SetCharacter(PlayerCharacter target)
    {
        character = new PlayerCharacter(target);
    }

    void ShowInfo()
    {
        face.sprite = Resources.Load<Sprite>(character.info.faceImgPath);
        charName.text = character.info.name;
        hp.value = character.battleStats.hp / character.battleStats.maxHp;
        mp.value = character.battleStats.mp / character.battleStats.maxMp;
        int nextLevelExp = Expression.GetExpExpression(character.level + 1);
        exp.value = character.experience / nextLevelExp;
        level.text = character.level.ToString("N0");
        toNextLevel.text = "To Next Level: " + (nextLevelExp - character.experience).ToString("N0");
    }

    void ShowAttributes()
    {
        str.OnShow((int)character.info.stats.strength, attributePoints);
        dex.OnShow((int)character.info.stats.dexterity, attributePoints);
        intel.OnShow((int)character.info.stats.intelligence, attributePoints);
        vit.OnShow((int)character.info.stats.vitality, attributePoints);
        end.OnShow((int)character.info.stats.endurance, attributePoints);
        wis.OnShow((int)character.info.stats.wisdom, attributePoints);
        if (attributePoints != 0)
        {
            points.text = attributePoints.ToString();
            availableText.gameObject.SetActive(true);
            points.gameObject.SetActive(true);
            confirm.gameObject.SetActive(true);
        }
        else
        {
            availableText.gameObject.SetActive(false);
            points.gameObject.SetActive(false);
            confirm.gameObject.SetActive(false);
        }
    }

    void ShowEquipments()
    {
        weapon.OnShowWeapon(character.weapon);
        weapon.SetTempChar(tempChar);
        int i;
        for(i = 0; i< character.armors.Count; i++)
        {
            armor[i].OnShowArmor(character.armors[i]);
            armor[i].SetTempChar(tempChar);
        }
        for (;i< armor.Length;i++)
        {
            armor[i].OnShowArmor(null);
            armor[i].SetTempChar(tempChar);
        }
    }

    void ShowDetailedStats()
    {
        detailedStats[0].OnUpdate(character.battleStats.hp, 0);
        detailedStats[1].OnUpdate(character.battleStats.mp, 0);
        detailedStats[2].OnUpdate(character.battleStats.patk, 0);
        detailedStats[3].OnUpdate(character.battleStats.matk, 0);
        detailedStats[4].OnUpdate(character.battleStats.pdef, 0);
        detailedStats[5].OnUpdate(character.battleStats.mdef, 0);
        detailedStats[6].OnUpdate(character.battleStats.spd, 0);
        detailedStats[7].OnUpdate(character.battleStats.eva, 0);
        detailedStats[8].OnUpdate(character.battleStats.crit, 0);
        detailedStats[9].OnUpdate(character.battleStats.fireRes, 0);
        detailedStats[10].OnUpdate(character.battleStats.lightningRes, 0);
        detailedStats[11].OnUpdate(character.battleStats.iceRes, 0);
        detailedStats[12].OnUpdate(character.battleStats.holyRes, 0);
        detailedStats[13].OnUpdate(character.battleStats.darkRes, 0);
        battlePointText.text = "Battle Point: " + character.bodyPower;
    }

    public void TestButton()
    {
        int pos = PlayerManager.Instance.ReturnCharacterPosById(charId);
        PlayerManager.Instance.Characters[pos].IncreaseLevel(1);
        character = PlayerManager.Instance.Characters[pos];
        Unhide(charId);
        OnShowSkill();
    }

    public void UpdateAvailablePoints()
    {
        points.text = attributePoints.ToString();
    }

    public void PreviewStatsFromAttributes()
    {
        tempChar.info.stats.strength = character.info.stats.strength + str.GetAddValue();
        tempChar.info.stats.dexterity = character.info.stats.dexterity + dex.GetAddValue();
        tempChar.info.stats.intelligence = character.info.stats.intelligence + intel.GetAddValue();
        tempChar.info.stats.vitality = character.info.stats.vitality + vit.GetAddValue();
        tempChar.info.stats.wisdom = character.info.stats.wisdom + wis.GetAddValue();
        tempChar.info.stats.endurance = character.info.stats.endurance + end.GetAddValue();
        tempChar.CalculateBattleStat();

        detailedStats[0].OnUpdate(character.battleStats.hp, 
            tempChar.battleStats.maxHp - character.battleStats.maxHp);
        detailedStats[1].OnUpdate(character.battleStats.mp, 
            tempChar.battleStats.maxMp - character.battleStats.maxMp);
        detailedStats[2].OnUpdate(character.battleStats.patk, 
            tempChar.battleStats.patk - character.battleStats.patk);
        detailedStats[3].OnUpdate(character.battleStats.matk, 
            tempChar.battleStats.matk - character.battleStats.matk);
        detailedStats[4].OnUpdate(character.battleStats.pdef, 
            tempChar.battleStats.pdef - character.battleStats.pdef);
        detailedStats[5].OnUpdate(character.battleStats.mdef, 
            tempChar.battleStats.mdef - character.battleStats.mdef);
        detailedStats[6].OnUpdate(character.battleStats.spd, 
            tempChar.battleStats.spd - character.battleStats.spd);
        detailedStats[7].OnUpdate(character.battleStats.eva, 
            tempChar.battleStats.eva - character.battleStats.eva);
        detailedStats[8].OnUpdate(character.battleStats.crit, 
            tempChar.battleStats.crit - character.battleStats.crit);
        detailedStats[9].OnUpdate(character.battleStats.fireRes, 
            tempChar.battleStats.fireRes - character.battleStats.fireRes);
        detailedStats[10].OnUpdate(character.battleStats.lightningRes, 
            tempChar.battleStats.lightningRes - character.battleStats.lightningRes);
        detailedStats[11].OnUpdate(character.battleStats.iceRes, 
            tempChar.battleStats.iceRes - character.battleStats.iceRes);
        detailedStats[12].OnUpdate(character.battleStats.holyRes, 
            tempChar.battleStats.holyRes - character.battleStats.holyRes);
        detailedStats[13].OnUpdate(character.battleStats.darkRes, 
            tempChar.battleStats.darkRes - character.battleStats.darkRes);
        battlePointText.text = "Battle Point: " + tempChar.bodyPower;
    }

    public void CancelAttributes()
    {
        tempChar = new PlayerCharacter(character);
        RefreshAll();
    }

    public void ConfirmAttributes()
    {
        int pos = PlayerManager.Instance.ReturnCharacterPosById(charId);
        if (pos != -1)
        {
            character.availableAttributes = attributePoints;
            character.info.stats.strength += str.GetAddValue();
            character.info.stats.dexterity += dex.GetAddValue();
            character.info.stats.intelligence += intel.GetAddValue();
            character.info.stats.vitality += vit.GetAddValue();
            character.info.stats.wisdom += wis.GetAddValue();
            character.info.stats.endurance += end.GetAddValue();
            PlayerManager.Instance.Characters[pos] = character;
            Unhide(charId);
            str.RefreshAll();
            dex.RefreshAll();
            intel.RefreshAll();
            vit.RefreshAll();
            wis.RefreshAll();
            end.RefreshAll();
        }
        else
        {
            Debug.LogError("Character with Id " + charId + " is not found in Player Character List.");
        }
    }

    public void RefreshAll()
    {
        str.RefreshAll();
        dex.RefreshAll();
        intel.RefreshAll();
        vit.RefreshAll();
        wis.RefreshAll();
        end.RefreshAll();
        detailedStats[0].OnUpdate(character.battleStats.hp, 0);
        detailedStats[1].OnUpdate(character.battleStats.mp, 0);
        detailedStats[2].OnUpdate(character.battleStats.patk, 0);
        detailedStats[3].OnUpdate(character.battleStats.matk, 0);
        detailedStats[4].OnUpdate(character.battleStats.pdef, 0);
        detailedStats[5].OnUpdate(character.battleStats.mdef, 0);
        detailedStats[6].OnUpdate(character.battleStats.spd, 0);
        detailedStats[7].OnUpdate(character.battleStats.eva, 0);
        detailedStats[8].OnUpdate(character.battleStats.crit, 0);
        detailedStats[9].OnUpdate(character.battleStats.fireRes, 0);
        detailedStats[10].OnUpdate(character.battleStats.lightningRes, 0);
        detailedStats[11].OnUpdate(character.battleStats.iceRes, 0);
        detailedStats[12].OnUpdate(character.battleStats.holyRes, 0);
        detailedStats[13].OnUpdate(character.battleStats.darkRes, 0);
        battlePointText.text = "Battle Point: " + character.bodyPower;
    }

    public void OnShowSkill()
    {
        skillPanel.OnShow(charId);
    }
}
