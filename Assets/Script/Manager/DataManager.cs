using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

using JsonDataClasses;

public class DataManager : MonoBehaviour
{
    public List<EnemyJson> EnemyList { get; private set; }
    public List<CharacterJson> CharacterList { get; private set; }
    public List<WeaponJson> WeaponList { get; private set; }
    public List<ArmorJson> ArmorList { get; private set; }
    public List<UsableJson> UsableList { get; private set; }
    public List<SkillJson> SkillList { get; private set; }

    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        //Check if there is any Instance. If not, create one, else destroy Object.
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(this);
        }
    }

    void Init()
    {
        //Read every data from Json and add them to correct List.
        EnemyList = new List<EnemyJson>();
        CharacterList = new List<CharacterJson>();
        WeaponList = new List<WeaponJson>();
        ArmorList = new List<ArmorJson>();
        UsableList = new List<UsableJson>();
        SkillList = new List<SkillJson>();

        var enemyJson = Resources.Load<TextAsset>("JSON/enemies");
        EnemyJson[] tempEnemyList = JsonConvert.DeserializeObject<EnemyJson[]>(enemyJson.text);
        EnemyList.AddRange(tempEnemyList);

        var characterJson = Resources.Load<TextAsset>("JSON/characters");
        CharacterJson[] tempCharacterList = JsonConvert.DeserializeObject<CharacterJson[]>(characterJson.text);
        CharacterList.AddRange(tempCharacterList);

        var weaponJson = Resources.Load<TextAsset>("JSON/weapons");
        WeaponJson[] tempWeaponList = JsonConvert.DeserializeObject<WeaponJson[]>(weaponJson.text);
        WeaponList.AddRange(tempWeaponList);

        for (int i = 0; i < WeaponList.Count; i++)
        {
            if (WeaponList[i].modification == null)
            {
                WeaponList[i].modification = new EquipmentModJson();
            }
            if (WeaponList[i].stats == null)
            {
                WeaponList[i].stats = new EquipmentStatJson();
            }
            WeaponList[i].GenerateDurability();
        }

        var armorJson = Resources.Load<TextAsset>("JSON/armors");
        ArmorJson[] tempArmorList = JsonConvert.DeserializeObject<ArmorJson[]>(armorJson.text);
        ArmorList.AddRange(tempArmorList);

        for (int i = 0; i < ArmorList.Count; i++)
        {
            if (ArmorList[i].modification == null)
            {
                ArmorList[i].modification = new EquipmentModJson();
            }
            if (ArmorList[i].stats == null)
            {
                ArmorList[i].stats = new EquipmentStatJson();
            }
            ArmorList[i].GenerateDurability();
        }

        var usableJson = Resources.Load<TextAsset>("JSON/usables");
        UsableJson[] tempUsableList = JsonConvert.DeserializeObject<UsableJson[]>(usableJson.text);
        UsableList.AddRange(tempUsableList);

        var skillJson = Resources.Load<TextAsset>("JSON/skills");
        SkillJson[] tempSkillList = JsonConvert.DeserializeObject<SkillJson[]>(skillJson.text);
        SkillList.AddRange(tempSkillList);
        
    }

    public WeaponJson SearchWeaponID (string id)
    {
        return WeaponList.Find((obj) => obj.id.Equals(id));
    }

    public ArmorJson SearchArmorID(string id)
    {
        return ArmorList.Find((obj) => obj.id.Equals(id));
    }

    public UsableJson SearchUsableID(string id)
    {
        return UsableList.Find((obj) => obj.id.Equals(id));
    }

    public SkillJson SearchSkillID (string id)
    {
        return SkillList.Find((obj) => obj.id.Equals(id));
    }
}
