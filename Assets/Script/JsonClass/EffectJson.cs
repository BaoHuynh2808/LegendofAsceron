using System;

namespace JsonDataClasses
{
    [Serializable]
    public class EffectJson
    {
        public string name;
        public string tooltips;
        public int power;
        public int duration;
        public EffectType type;
        public EffectAffection affection;
    }

    public enum EffectAffection
    {
        HpChange,
        PatkChange,
        MatkChange,
        PdefChange,
        MdefChange
    }

    public enum EffectType
    {
        Buff,
        Debuff
    }
}
