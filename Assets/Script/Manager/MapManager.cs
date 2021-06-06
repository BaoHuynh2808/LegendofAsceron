using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

using JsonDataClasses;

public class MapManager : MonoBehaviour {
	public static MapManager _instance;
    public Dictionary<string,MapJson> mapList = new Dictionary<string, MapJson>();

    public string BattleSceneID { get; set; }
    public string NewGameSceneID { get; set; }

    public static MapManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
            Initialize();
        }
    }

    void Initialize()
    {
        var mapJson = Resources.Load<TextAsset>("JSON/maps");
        MapJson[] tempMapList = JsonConvert.DeserializeObject<MapJson[]>(mapJson.text);
        for (int i = 0; i < tempMapList.Length; i++)
        {
            mapList.Add(tempMapList[i].id, tempMapList[i]);
        }
        BattleSceneID = "M0001";
        NewGameSceneID = "M0002";
    }
}
