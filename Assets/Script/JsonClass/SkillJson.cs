using System;

namespace JsonDataClasses
{
    [Serializable]
    public class SkillJson
    {
        public string id;
        public string name;
        public string tooltips;
        public bool isPassive;
        public int mpCost;
        public int lvlRequirement;
        public SkillTarget[] target;
        public SkillType[] type;
        public int power;
        public EffectJson effect;
        public string imgPath;
    }

    public enum SkillTarget
    {
        Self,
        Ally,
        Allies,
        Enemy,
        Enemies
    }

   public enum SkillType
   {
        Damage,
        DoT,
        Heal,
        Debuff,
        Buff
   }
}
