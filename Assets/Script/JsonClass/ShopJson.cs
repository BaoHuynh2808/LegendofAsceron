using System;

namespace JsonDataClasses
{
    [Serializable]
    public class ShopJson
    {
        public string id;
        public Dialog shopDialog;
        public string[] shopItemIds;
    }
}