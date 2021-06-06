using System;

namespace JsonDataClasses
{
    [Serializable]
    public class EnemyJson : UnitJson
    {
        public UnitStatJson growthStat;
        public Reward baseReward;
        public Reward growthReward;
        public string imgPath;
		public string facePath;
    }

    [Serializable]
    public class Reward
    {
        public int gold;
        public int exp;
        public ItemReward[] item;
    }

    [Serializable]
    public class ItemReward
    {
        public string itemID;
        public float procChance;
    }
}