using System;

namespace JsonDataClasses
{
    [Serializable]
    public class UnitJson
    {
        public string id;
        public string name;
        public string description;
        public UnitRace race;
        public UnitType type;
        public UnitStatJson stats;
        public string[] learnedSkills;
        public string[] totalSkills;
        public string modelImgPath;
        public string faceImgPath;
    }

    public enum UnitType
    {
        Melee, Range
    }

    public enum UnitRace
    {
        Undead, Humanoid
    }
}
