using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JsonDataClasses;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private static PlayerManager _instance;

    public int PlayTime { get; set; }
    public int Difficulty { get; set; }
    public List<PlayerWeapon> Weapons { get; private set; }
    public List<PlayerArmor> Armors { get; private set; }
    public List<PlayerUsable> Usables { get; private set; }
    public int Currency { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
    //Rotation as Euler Angles
    public float RotX { get; set; }
    public float RotY { get; set; }
    public float RotZ { get; set; }
    public string CurrentSceneID { get; set; }
    public bool CheckNewGame { get; set; }
    public List<PlayerCharacter> Characters { get; private set; }
    public List<string> PartyMemberID { get; private set; }

    public GameObject player;
    private const float timeInterval = 1f;

    public static PlayerManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
            StartCoroutine(UpdatePlayerValue());
            DontDestroyOnLoad(this);
        }
    }

    IEnumerator UpdatePlayerValue()
    {
        while(true)
        {
            PlayTime += (int)timeInterval;
            if (SceneManager.GetActiveScene().name != "M0001-Battle")
            {
                RotX = player.transform.rotation.eulerAngles.x;
                RotY = player.transform.rotation.eulerAngles.y;
                RotZ = player.transform.rotation.eulerAngles.z;
            }
            yield return new WaitForSeconds(timeInterval);
        }
    }

    public void Initialize(int diff)
    {
        PlayTime = 0;
        Difficulty = diff;
        Weapons = new List<PlayerWeapon>();
        Armors = new List<PlayerArmor>();
        Usables = new List<PlayerUsable>();
        Currency = 0;
        CheckNewGame = true;
        Characters = new List<PlayerCharacter>
        {
            new PlayerCharacter(DataManager.Instance.CharacterList[0], 1),
            new PlayerCharacter(DataManager.Instance.CharacterList[1], 1),
            new PlayerCharacter(DataManager.Instance.CharacterList[2], 1)
        };
        PartyMemberID = new List<string>
        {
            Characters[0].info.id,
            Characters[1].info.id,
            Characters[2].info.id
        };

        PosX = 0;
        PosY = 0;
        PosZ = 0;
        RotX = 0;
        RotY = 0;
        RotZ = 0;

        AddItem("armor", "IA0001", 1);
        AddItem("armor", "IA0002", 1);
        AddItem("armor", "IA0003", 1);
        AddItem("armor", "IA0004", 1);
        AddItem("usable", "IU0001", 1);

        PlayerCharacter character = Characters[0];
        EquipEquipment("armor", "IA0001", ref character);
        Characters[0] = character;

        character = Characters[1];
        EquipEquipment("armor", "IA0002", ref character);
        EquipEquipment("armor", "IA0003", ref character);
        Characters[1] = character;

        CurrentSceneID = "M0000";
        //Characters[0].UnequipArmor(ArmorPiece.Head);

        Debug.Log("Check");
    }

    public void AddCharacter(CharacterJson character, int level)
    {
        Characters.Add(new PlayerCharacter(character, level));
    }

    public PlayerCharacter SearchCharacterById(string id)
    {
        return Characters.Find((obj) => obj.info.id.Equals(id));
    }

    public int ReturnCharacterPosById(string id)
    {
        for (int i=0; i<Characters.Count; i++)
        {
            if ( Characters[i].info.id == id)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddToParty(string id)
    {
        if (PartyMemberID.Count < 3)
        {
            PartyMemberID.Add(id);
        }
    }

    public void RemoveFromParty(string id)
    {
        PartyMemberID.Remove(id);
    }

    public void SwapPartyMember(int index, string selectId)
    {
        if (index < PartyMemberID.Count)
        {
            PartyMemberID[index] = selectId;
        }
    }

    public void AddItem(string type, string id, int quantity)
    {
        if (type.ToLower() == "usable")
        {
            if (DataManager.Instance.SearchUsableID(id) != null)
            {
                for (int i=0; i<Usables.Count;i++)
                {
                    if (Usables[i].usable.id == id)
                    {
                        Usables[i].amount += quantity;
                        return;
                    }
                }
                Usables.Add(new PlayerUsable(id, quantity));
            }
            else
            {
                Debug.LogError("Usable " + id + " not found!");
            }
        }
        else if (type.ToLower() == "weapon")
        {
            if (DataManager.Instance.SearchWeaponID(id) != null)
            {
                Weapons.Add(new PlayerWeapon(id));
            }
            else
            {
                Debug.LogError("Weapon " + id + " not found!");
            }
        }
        else if (type.ToLower() == "armor")
        {
            if (DataManager.Instance.SearchArmorID(id) != null)
            {
                Armors.Add(new PlayerArmor(id));
            }
            else 
            {
                Debug.LogError("Armor " + id + " not found!");
            }
        }
    }

    public void EquipEquipment(string equipmentType, string equipmentId,ref PlayerCharacter character)
    {
        if (equipmentType.ToLower() == "weapon")
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i].weapon.id == equipmentId)
                {
                    PlayerWeapon playerWeapon = Weapons[i];
                    character.EquipWeapon(ref playerWeapon);
                    Weapons[i] = playerWeapon;
                    break;
                }
            }
        }
        else if (equipmentType.ToLower() == "armor")
        {
            for (int i = 0; i < Armors.Count; i++)
            {
                if (Armors[i].armor.id == equipmentId)
                {
                    PlayerArmor playerArmor = Armors[i];
                    character.EquipArmor(ref playerArmor);
                    Armors[i] = playerArmor;
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("Target Item is not an Equipment!");
        }
    }

    public void UnequipEquipment(string equipmentId, PlayerCharacter character)
    {

    }

    public void SetPlayerXYZ(float x, float y, float z)
    {
        PosX = x;
        PosY = y;
        PosZ = z;
    }

    public void SetPlayerPos()
    {
        player.transform.position = new Vector3(PosX,PosY,PosZ);
        player.transform.rotation = Quaternion.Euler(RotX,RotY,RotZ);
    }

    public void SetPlayerData(PlayerData data)
    {
        Difficulty = data.difficulty;
        Currency = data.currency;
        PlayTime = data.playTime;
        Characters = data.characters;
        PartyMemberID = data.partyMemberIds;
        Weapons = data.weapons;
        Armors = data.armors;
        Usables = data.usables;
        PosX = data.posX;
        PosY = data.posY;
        PosZ = data.posZ;
        RotX = data.rotX;
        RotY = data.rotY;
        RotZ = data.rotZ;
        CurrentSceneID = data.sceneID;
        CheckNewGame = data.newGame;
		Debug.Log(PosX + " " + PosY + " " + PosZ + "Check Load");
    }

    public void UpdateCurrentSceneID(string scene)
    {
        CurrentSceneID = scene;
    }
}

[Serializable]
public class PlayerData
{
    public int playTime;
    public int difficulty;
    public List<PlayerCharacter> characters;
    public List<string> partyMemberIds;
    public List<PlayerWeapon> weapons;
    public List<PlayerArmor> armors;
    public List<PlayerUsable> usables;
    public int currency;
    public float posX;
    public float posY;
    public float posZ;
    public float rotX;
    public float rotY;
    public float rotZ;
    public string sceneID;
    public bool newGame;

    public PlayerData()
    {
        playTime = PlayerManager.Instance.PlayTime;
        difficulty = PlayerManager.Instance.Difficulty;
        currency = PlayerManager.Instance.Currency;
        characters = PlayerManager.Instance.Characters;
        partyMemberIds = PlayerManager.Instance.PartyMemberID;
        weapons = PlayerManager.Instance.Weapons;
        armors = PlayerManager.Instance.Armors;
        usables = PlayerManager.Instance.Usables;
        posX = PlayerManager.Instance.PosX;
        posY = PlayerManager.Instance.PosY;
        posZ = PlayerManager.Instance.PosZ;
        rotX = PlayerManager.Instance.RotX;
        rotY = PlayerManager.Instance.RotY;
        rotZ = PlayerManager.Instance.RotZ;
        sceneID = PlayerManager.Instance.CurrentSceneID;
        newGame = PlayerManager.Instance.CheckNewGame;
    }
}

[Serializable]
public class PlayerCharacter
{
    public int level;
    public int experience;
    public int availableAttributes;
    public int availableSkillPoints;
    public PlayerCharBattleStat battleStats;
    public UnitJson info;
    public WeaponType[] weaponTypes;
    public PlayerWeapon weapon;
    public List<PlayerArmor> armors;
    public int bodyPower;

    public PlayerCharacter(CharacterJson json, int targetLevel)
    {
        info = new UnitJson
        {
            id = json.id,
            name = json.name,
            description = json.description,
            race = json.race,
            type = json.type,
            learnedSkills = json.learnedSkills,
            totalSkills = json.totalSkills,
            modelImgPath = json.modelImgPath,
            faceImgPath = json.faceImgPath,
            stats = new UnitStatJson()
        };

        weaponTypes = json.weaponTypes;

        //if (!String.IsNullOrEmpty(json.weaponID))
        //{
        weapon = new PlayerWeapon(json.weaponID);
        //}

        // (json.armorIDs.Length > 0)
        //{
        armors = new List<PlayerArmor>();
        for (int i = 0; i < json.armorIDs.Length; i++)
        {
            armors.Add(new PlayerArmor(json.armorIDs[i]));
        }
        //}

        SetStatByLevel(targetLevel, json);

        bodyPower = 0;

        availableAttributes = 0;
        availableSkillPoints = 0;

        CalculateBattleStat();
        RefreshStatus();
    }

    public PlayerCharacter(PlayerCharacter target)
    {
        info = new UnitJson();
        info.id = target.info.id;
        info.name = target.info.name;
        info.description = target.info.description;
        info.faceImgPath = target.info.faceImgPath;
        info.learnedSkills = target.info.learnedSkills;
        info.modelImgPath = target.info.modelImgPath;
        info.race = target.info.race;
        info.totalSkills = target.info.totalSkills;
        info.type = target.info.type;
        info.stats = new UnitStatJson();
        info.stats.hp = target.info.stats.hp;
        info.stats.mp = target.info.stats.mp;
        info.stats.strength = target.info.stats.strength;
        info.stats.dexterity = target.info.stats.dexterity;
        info.stats.intelligence = target.info.stats.intelligence;
        info.stats.vitality = target.info.stats.vitality;
        info.stats.endurance = target.info.stats.endurance;
        info.stats.wisdom = target.info.stats.wisdom;
        info.stats.level = target.info.stats.level;
        info.stats.fireRes = target.info.stats.fireRes;
        info.stats.lightningRes = target.info.stats.lightningRes;
        info.stats.iceRes = target.info.stats.iceRes;
        info.stats.holyRes = target.info.stats.holyRes;
        info.stats.darkRes = target.info.stats.darkRes;
        if (target.weapon != null && target.weapon.weapon != null)
        {
            weapon = new PlayerWeapon(target.weapon.weapon.id);
        }
        armors = new List<PlayerArmor>();
        for (int i = 0; i < target.armors.Count; i++)
        {
            if (target.armors[i] != null && target.armors[i].armor != null)
            {
                armors.Add(new PlayerArmor(target.armors[i].armor.id));
            }
        }

        level = target.level;
        experience = target.experience;

        bodyPower = target.bodyPower;

        availableAttributes = target.availableAttributes;
        availableSkillPoints = target.availableSkillPoints;

        CalculateBattleStat();
        RefreshStatus();
    }


    public void UnequipWeapon()
    {
        weapon.owner = null;
        weapon = null;
        CalculateBattleStat();
    }

    public void UnequipArmor(ArmorPiece piece)
    {
        for (int i = 0; i < armors.Count; i++)
        {
            if (armors[i].armor.aPiece == piece)
            {
                armors[i].owner = null;
                armors.Remove(armors[i]);
                CalculateBattleStat();
                return;
            }
        }
    }

    public void EquipWeapon(ref PlayerWeapon equipment)
    {
        if(weapon != null)
        {
            weapon.owner = null;
        }
        if(equipment.owner != null)
        {
            equipment.owner.UnequipWeapon();
            equipment.owner.CalculateBattleStat();
        }
        equipment.owner = this;
        weapon = equipment;
    }

    public void EquipArmor(ref PlayerArmor piece)
    {
        for (int i = 0; i < armors.Count; i++)
        {
            if ( armors[i].armor.aPiece == piece.armor.aPiece)
            {
                armors[i].owner = null;
                if(piece.owner != null)
                {
                    piece.owner.UnequipArmor(piece.armor.aPiece);
                    piece.owner.CalculateBattleStat();
                }
                piece.owner = this;
                armors.Remove(armors[i]);
                armors.Add(piece);
                CalculateBattleStat();
                return;
            }
        }
        armors.Add(piece);
        piece.owner = this;
        CalculateBattleStat();
    }

    public void AddExperience(int exp)
    {
        experience += exp;
        int nextLevelExp = Expression.GetExpExpression(level + 1);
        if (experience >= nextLevelExp)
        {
            experience = experience - nextLevelExp;
            IncreaseLevel(1);
            CalculateBattleStat();
            RefreshStatus();
        }
    }

    public void RefreshStatus()
    {
        battleStats.hp = battleStats.maxHp;
        battleStats.mp = battleStats.maxMp;
    }

    public void SetHp(float targetHp)
    {
        battleStats.hp = (targetHp >= battleStats.maxHp) ? battleStats.maxHp : targetHp;
    }

    public void SetMp(float targetMp)
    {
        battleStats.mp = (targetMp >= battleStats.maxMp) ? battleStats.maxMp : targetMp;
    }

    void AddWeaponStat()
    {
        if (weapon.durability > 0)
        {

            battleStats.maxHp += weapon.weapon.stats.vitality * GameConfigs.HP_PER_VIT;
            battleStats.maxMp += weapon.weapon.stats.wisdom * GameConfigs.MP_PER_WIS;

            battleStats.eva += weapon.weapon.stats.dexterity * GameConfigs.EVA_PER_DEX;
            battleStats.crit += weapon.weapon.stats.dexterity * GameConfigs.CRIT_PER_DEX;
            battleStats.spd += weapon.weapon.stats.dexterity * GameConfigs.SPD_PER_DEX;

            battleStats.patk += weapon.weapon.patk + weapon.weapon.stats.strength * GameConfigs.PATK_PER_STR;
            battleStats.matk += weapon.weapon.matk + weapon.weapon.stats.intelligence * GameConfigs.MATK_PER_INT;

            battleStats.pdef += weapon.weapon.stats.endurance * GameConfigs.PDEF_PER_END;
            battleStats.mdef += weapon.weapon.stats.wisdom * GameConfigs.MDEF_PER_WIS;

            battleStats.fireRes += weapon.weapon.stats.fireRes;
            battleStats.lightningRes += weapon.weapon.stats.lightningRes;
            battleStats.iceRes += weapon.weapon.stats.iceRes;
            battleStats.holyRes += weapon.weapon.stats.holyRes;
            battleStats.darkRes += weapon.weapon.stats.darkRes;

            if (weapon.weapon.modification != null)
            {
                battleStats.maxHp += weapon.weapon.modification.hp;
                battleStats.maxMp += weapon.weapon.modification.mp;

                battleStats.eva += weapon.weapon.modification.evasion;
                battleStats.crit += weapon.weapon.modification.crit;
                battleStats.spd += weapon.weapon.modification.speed;

                battleStats.patk += weapon.weapon.modification.patk;
                battleStats.matk += weapon.weapon.modification.matk;

                battleStats.pdef += weapon.weapon.modification.pdef;
                battleStats.mdef += weapon.weapon.modification.mdef;
            }
        }
    }

    void AddArmorStat(PlayerArmor piece)
    {
        if (piece.durability > 0)
        {
            battleStats.maxHp += piece.armor.stats.vitality * GameConfigs.HP_PER_VIT;
            battleStats.maxMp += piece.armor.stats.wisdom * GameConfigs.MP_PER_WIS;

            battleStats.eva += piece.armor.stats.dexterity * GameConfigs.EVA_PER_DEX;
            battleStats.crit += piece.armor.stats.dexterity * GameConfigs.CRIT_PER_DEX;
            battleStats.spd += piece.armor.stats.dexterity * GameConfigs.SPD_PER_DEX;

            battleStats.patk += piece.armor.stats.strength * GameConfigs.PATK_PER_STR;
            battleStats.matk += piece.armor.stats.intelligence * GameConfigs.MATK_PER_INT;

            battleStats.pdef += piece.armor.pdef + piece.armor.stats.endurance * GameConfigs.PDEF_PER_END;
            battleStats.mdef += piece.armor.mdef + piece.armor.stats.wisdom * GameConfigs.MDEF_PER_WIS;

            battleStats.fireRes += piece.armor.stats.fireRes;
            battleStats.lightningRes += piece.armor.stats.lightningRes;
            battleStats.iceRes += piece.armor.stats.iceRes;
            battleStats.holyRes += piece.armor.stats.holyRes;
            battleStats.darkRes += piece.armor.stats.darkRes;

            if (piece.armor.modification != null)
            {
                battleStats.maxHp += piece.armor.modification.hp;
                battleStats.maxMp += piece.armor.modification.mp;

                battleStats.eva += piece.armor.modification.evasion;
                battleStats.crit += piece.armor.modification.crit;
                battleStats.spd += piece.armor.modification.speed;

                battleStats.patk += piece.armor.modification.patk;
                battleStats.matk += piece.armor.modification.matk;

                battleStats.pdef += piece.armor.modification.pdef;
                battleStats.mdef += piece.armor.modification.mdef;
            }
        }
    }

    public void CalculateBattleStat()
    {
        PlayerCharBattleStat tempBattleStats = new PlayerCharBattleStat();
        bodyPower = 0;

        if (battleStats != null)
        {
            tempBattleStats.hp = battleStats.hp;
            tempBattleStats.mp = battleStats.mp;
        }

        battleStats = tempBattleStats;

        battleStats.maxHp = info.stats.hp + info.stats.vitality * GameConfigs.HP_PER_VIT;
        battleStats.maxMp = info.stats.mp + info.stats.wisdom * GameConfigs.MP_PER_WIS;

        battleStats.eva = info.stats.dexterity * GameConfigs.EVA_PER_DEX;
        battleStats.crit = info.stats.dexterity * GameConfigs.CRIT_PER_DEX;
        battleStats.spd = info.stats.dexterity * GameConfigs.SPD_PER_DEX;

        battleStats.patk = info.stats.strength * GameConfigs.PATK_PER_STR;
        battleStats.matk = info.stats.intelligence * GameConfigs.MATK_PER_INT;

        battleStats.pdef = info.stats.endurance * GameConfigs.PDEF_PER_END;
        battleStats.mdef = info.stats.wisdom * GameConfigs.MDEF_PER_WIS;

        battleStats.fireRes = info.stats.fireRes;
        battleStats.lightningRes = info.stats.lightningRes;
        battleStats.iceRes = info.stats.iceRes;
        battleStats.holyRes = info.stats.holyRes;
        battleStats.darkRes = info.stats.darkRes;

        if (weapon != null)
        {
            AddWeaponStat();
        }

        if (armors != null)
        {
            for (int i = 0; i < armors.Count && armors[i].armor != null; i++)
            {
                AddArmorStat(armors[i]);
            }
        }

        battleStats.eva = (battleStats.eva <= GameConfigs.MAX_EVA_PERCENTAGE) ?
            battleStats.eva : GameConfigs.MAX_EVA_PERCENTAGE;
        battleStats.crit = (battleStats.crit <= GameConfigs.MAX_CRIT_PERCENTAGE) ?
            battleStats.crit : GameConfigs.MAX_CRIT_PERCENTAGE;
        if (battleStats.hp >= battleStats.maxHp)
        {
            battleStats.hp = battleStats.maxHp;
        }
        if (battleStats.mp >= battleStats.maxMp)
        {
            battleStats.mp = battleStats.maxMp;
        }

        bodyPower += (int)battleStats.maxHp * BodyPower.HP;
        bodyPower += (int)battleStats.maxMp * BodyPower.MP;
        bodyPower += (int)battleStats.eva * BodyPower.EVA;
        bodyPower += (int)battleStats.crit * BodyPower.CRIT;
        bodyPower += (int)battleStats.spd * BodyPower.SPD;
        bodyPower += (int)battleStats.patk * BodyPower.PATK;
        bodyPower += (int)battleStats.matk * BodyPower.MATK;
        bodyPower += (int)battleStats.pdef * BodyPower.PDEF;
        bodyPower += (int)battleStats.mdef * BodyPower.MDEF;
        bodyPower += (int)battleStats.fireRes * BodyPower.ELE_RES;
        bodyPower += (int)battleStats.lightningRes * BodyPower.ELE_RES;
        bodyPower += (int)battleStats.iceRes * BodyPower.ELE_RES;
        bodyPower += (int)battleStats.holyRes * BodyPower.MORAL_RES;
        bodyPower += (int)battleStats.darkRes * BodyPower.MORAL_RES;
    }

    public void IncreaseAttributes(int amount)
    {
        availableAttributes += amount;
    }

    public void IncreaseSkillPoints(int amount)
    {
        availableSkillPoints += amount;
    }

    public void IncreaseLevel(int increaseAmount)
    {
        level += increaseAmount;
        IncreaseAttributes(GameConfigs.ATTRIBUTES_PER_LVL * increaseAmount);
        IncreaseSkillPoints(GameConfigs.SKILLPOINTS_PER_LVL * increaseAmount);
    }

    public bool IsSkillLeanred(string skillId)
    {
        for (int i=0; i< info.learnedSkills.Length; i++)
        {
            if (info.learnedSkills[i] == skillId)
                return true;
        }
        return false;
    }

    public bool IsSkillAvailable(string skillId)
    {
        for (int i=0; i< info.totalSkills.Length; i++)
        {
            if (info.totalSkills[i] == skillId)
                return true;
        }
        return false;
    }

    public void AddSkill(string skillId)
    {
        if (IsSkillAvailable(skillId))
        {
            string[] temp = new string[info.learnedSkills.Length+1];
            for (int i=0; i<info.learnedSkills.Length; i++)
            {
                temp[i] = info.learnedSkills[i];
            }
            temp[info.learnedSkills.Length] = skillId;
            info.learnedSkills = temp;
            availableSkillPoints--;
        }
    }

    public void SetStatByLevel(int lvl, CharacterJson character)
    {
        if (lvl > 0)
        {
            level = lvl;
            experience = 0;
            info.stats.hp = character.stats.hp + (character.growthStat.hp * (lvl - 1));
            info.stats.mp = character.stats.mp + (character.growthStat.mp * (lvl - 1));
            info.stats.strength = character.stats.strength + (character.growthStat.strength * (lvl - 1));
            info.stats.dexterity = character.stats.dexterity + (character.growthStat.dexterity * (lvl - 1));
            info.stats.intelligence = character.stats.intelligence + (character.growthStat.intelligence * (lvl - 1));
            info.stats.vitality = character.stats.vitality + (character.growthStat.vitality * (lvl - 1));
            info.stats.wisdom = character.stats.wisdom + (character.growthStat.wisdom * (lvl - 1));
            info.stats.endurance = character.stats.endurance + (character.growthStat.endurance * (lvl - 1));
        }
        info.stats.fireRes = character.stats.fireRes;
        info.stats.lightningRes = character.stats.lightningRes;
        info.stats.iceRes = character.stats.iceRes;
        info.stats.holyRes = character.stats.holyRes;
        info.stats.darkRes = character.stats.darkRes;
    }
}

[Serializable]
public class PlayerWeapon 
{
    public WeaponJson weapon;
    public int durability;
    public PlayerCharacter owner;

    public PlayerWeapon(string weaponID)
    {
        weapon = DataManager.Instance.SearchWeaponID(weaponID);
        if (weapon != null)
        {
            weapon.GenerateDurability();
            durability = weapon.MaxDurability;
            owner = null;
        }
    }
}

[Serializable]
public class PlayerArmor
{
    public ArmorJson armor;
    public int durability;
    public PlayerCharacter owner;

    public PlayerArmor(string armorID)
    {
        armor = DataManager.Instance.SearchArmorID(armorID);
        if (armor != null)
        {
            armor.GenerateDurability();
            durability = armor.MaxDurability;
            owner = null;
        }
    }
}

[Serializable]
public class PlayerUsable
{
    public UsableJson usable;
    public int amount;

    public PlayerUsable(string usableID, int quantity)
    {
        usable = DataManager.Instance.SearchUsableID(usableID);
        amount = quantity;
    }
}

[Serializable]
public class PlayerCharBattleStat
{
    public float maxHp;
    public float maxMp;
    public float hp;
    public float mp;
    public float patk;
    public float matk;
    public float pdef;
    public float mdef;
    public float spd;
    public float eva;
    public float crit;
    public float fireRes;
    public float lightningRes;
    public float iceRes;
    public float holyRes;
    public float darkRes;

    public PlayerCharBattleStat()
    {
        maxHp = 0;
        maxMp = 0;
        patk = 0;
        matk = 0;
        pdef = 0;
        mdef = 0;
        spd = 0;
        eva = 0;
        crit = 0;
        fireRes = 0;
        lightningRes = 0;
        iceRes = 0;
        holyRes = 0;
        darkRes = 0;
    }
}