using System;

namespace JsonDataClasses
{
    [Serializable]
    public class SelectionsJson
    {
        public string id;
        public Dialog selectionDialog;
        public string[] dialogType;
        public string[] talkId;
        public string shopId;
        public string questId;
    }
}