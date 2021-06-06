using System;

namespace JsonDataClasses
{
    [Serializable]
    public class MapJson
    {
        public string id;
        public string sceneName;
        public int sceneId;
        public int minLv;
        public string[] enemyIDs;
    }
}