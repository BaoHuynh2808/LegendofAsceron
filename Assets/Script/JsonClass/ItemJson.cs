using System;

namespace JsonDataClasses
{
    [Serializable]
    public class ItemJson
    {
        public string id;
        public string name;
        public int lvlRequirement;
        public ItemType type;
        public string tooltips;
        public string imgPath;
        public int gold;
    }

    [Serializable]
    public class UsableJson : ItemJson
    {
        public int amount;
        public bool isPercentage;
        public UsableRegenType regenType;
    }

    [Serializable]
    public class ArmorJson : EquipmentJson
    {
        public ArmorType aType;
        public ArmorPiece aPiece;
        public int pdef;
        public int mdef;

        protected override float EquipmentTypeDurability()
        {
            float typeDurability = 0;
            switch (aType)
            {
                case ArmorType.Heavy:
                    typeDurability = ArmorTypeDur.heavy;
                    break;
                case ArmorType.Clothing:
                    typeDurability = ArmorTypeDur.clothing;
                    break;
                default:
                    typeDurability = ArmorTypeDur.light;
                    break;
            }

            float pieceDurability = 0;
            switch(aPiece)
            {
                case ArmorPiece.Chest:
                    pieceDurability = ArmorPieceDur.chest;
                    break;
                case ArmorPiece.Head:
                    pieceDurability = ArmorPieceDur.helmet;
                    break;
                case ArmorPiece.Gauntlets:
                    pieceDurability = ArmorPieceDur.gauntlets;
                    break;
                default:
                    pieceDurability = ArmorPieceDur.boots;
                    break;
            }

            return typeDurability * pieceDurability;
        }
    }

    [Serializable]
    public class WeaponJson : EquipmentJson
    {
        public WeaponType wType;
        public int patk;
        public int matk;

        protected override float EquipmentTypeDurability()
        {
            switch(wType)
            {
                case WeaponType.Sword:
                    return WeaponTypeDur.sword;
                case WeaponType.Axe:
                    return WeaponTypeDur.axe;
                case WeaponType.Crossbow:
                    return WeaponTypeDur.crossbow;
                case WeaponType.Grimoire:
                    return WeaponTypeDur.grimoire;
                default:
                    return WeaponTypeDur.gun;
            }
        }
    }

    [Serializable]
    public class EquipmentJson : ItemJson
    {
        public int ilvl;
        public EquipmentRarity rarity;
        public EquipmentModJson modification;
        public EquipmentStatJson stats;
        public int MaxDurability { get; private set; }

        protected virtual float EquipmentTypeDurability() 
        {
            return 0;
        }

        public void GenerateDurability()
        {
            float rarityCheck = 0;
            switch(rarity)
            {
                case EquipmentRarity.Uncommon:
                    rarityCheck = EquipmentQualityDur.uncommon;
                    break;
                case EquipmentRarity.Rare:
                    rarityCheck = EquipmentQualityDur.rare;
                    break;
                case EquipmentRarity.Mythical:
                    rarityCheck = EquipmentQualityDur.mythical;
                    break;
                case EquipmentRarity.Legendary:
                    rarityCheck = EquipmentQualityDur.legendary;
                    break;
                case EquipmentRarity.Relic:
                    rarityCheck = EquipmentQualityDur.relic;
                    break;
                default:
                    rarityCheck = EquipmentQualityDur.common;
                    break;
            }
            float equipmentDurability = EquipmentTypeDurability();
            MaxDurability = (int)((equipmentDurability + ilvl) * rarityCheck);
        }
    }

    [Serializable]
    public class EquipmentStatJson
    {
        public float strength;
        public float dexterity;
        public float intelligence;
        public float vitality;
        public float endurance;
        public float wisdom;
        public float fireRes;
        public float lightningRes;
        public float iceRes;
        public float holyRes;
        public float darkRes;

        public EquipmentStatJson()
        {
            strength = 0;
            dexterity = 0;
            intelligence = 0;
            vitality = 0;
            endurance = 0;
            wisdom = 0;
            fireRes = 0;
            lightningRes = 0;
            iceRes = 0;
            holyRes = 0;
            darkRes = 0;
        }
    }

    [Serializable]
    public class EquipmentModJson
    {
        public float hp;
        public float mp;
        public float patk;
        public float matk;
        public float speed;
        public float evasion;
        public float crit;
        public float mdef;
        public float pdef;

        public EquipmentModJson()
        {
            hp = 0;
            mp = 0;
            patk = 0;
            matk = 0;
            speed = 0;
            evasion = 0;
            crit = 0;
            mdef = 0;
            pdef = 0;
        }
    }
    
    public enum ArmorType
    {
        Heavy,
        Light,
        Clothing
    }

    public enum ArmorPiece
    {
        Head,
        Chest,
        Gauntlets,
        Boots
    }

    public enum WeaponType
    {
        Sword,
        Axe,
        Gun,
        Crossbow,
        Grimoire
    }

    public enum ItemType
    {
        Equipment,
        Usable,
        Recipe,
        Miscellaneous,
        Quest
    }

    public enum EquipmentRarity
    {
        Common,
        Uncommon,
        Rare,
        Mythical,
        Legendary,
        Relic
    }

    public enum UsableRegenType
    {
        HP,
        MP,
        Synergy
    }

    public static class EquipmentQualityDur
    {
        public const float common = 0.9f;
        public const float uncommon = 1.0f;
        public const float rare = 1.13f;
        public const float mythical = 1.28f;
        public const float legendary = 1.5f;
        public const float relic = 1.8f;
    }

    public static class WeaponTypeDur
    {
        public const float sword = 15;
        public const float grimoire = 10;
        public const float crossbow = 10;
        public const float axe = 20;
        public const float gun = 7;
    }

    public static class ArmorPieceDur
    {
        public const float chest = 30;
        public const float helmet = 20;
        public const float gauntlets = 15;
        public const float boots = 20;
    }

    public static class ArmorTypeDur
    {
        public const float heavy = 1.25f;
        public const float light = 1;
        public const float clothing = 0.75f;
    }
}