using System;

namespace JsonDataClasses
{
    [Serializable]
    public class DialogsJson
    {
        public string id;
        public string dialogType;
        public Dialog[] dialogs;
    }

    [Serializable]
    public class Dialog
    {
        public string name;
        public string context;
        public string imgPath;
    }

    [Serializable]
    public static class DialogType
    {
        public const string smallTalk = "Small Talk";
        public const string questTalk = "Quest";
        public const string eventTalk = "Event";
    }
}