using System;

namespace JsonDataClasses
{
    [Serializable]
    public class CharacterJson : UnitJson
    {
        public UnitStatJson growthStat;
        public WeaponType[] weaponTypes;
        public string weaponID;
        public string[] armorIDs;
    }
}