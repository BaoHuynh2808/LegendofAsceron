using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour {

    private static SaveLoadManager _instance;
    private const string FILE_EXTENSION = ".sav";
    private readonly string[] saveType = { "Autosave_", "ManualSave_" };
    private string savePath;
    //private int saveNumber = 0;

    private readonly int resWidth = 426;
    private readonly int resHeight = 240;
    public static SaveLoadManager Instance
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
            savePath = Application.persistentDataPath + "/saves/";
        }
    }

    public void Save(bool isAutosave)
    {
        PlayerManager.Instance.SetPlayerXYZ(
            PlayerManager.Instance.player.transform.position.x,
            PlayerManager.Instance.player.transform.position.y,
            PlayerManager.Instance.player.transform.position.z);

        if(!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string saveTime = System.DateTime.Now.ToString("yyyyMMddHHmmss_");

        int sec = PlayerManager.Instance.PlayTime % 60;
        int min = (PlayerManager.Instance.PlayTime / 60) % 60;
        int hour = (PlayerManager.Instance.PlayTime / 60) / 60;
        string playTime = hour.ToString("D4") + min.ToString("D2") + sec.ToString("D2") +"_";

        string difficulty = PlayerManager.Instance.Difficulty + "_";

        string fileName = ((isAutosave) ? saveType[0] : saveType[1]) + difficulty + playTime + saveTime; //+ hexNumToString;
        string path = savePath + fileName;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path + FILE_EXTENSION, FileMode.Create);
		PlayerData data = new PlayerData();
		bf.Serialize(stream, data);
		Debug.Log("Check: " + PlayerManager.Instance.PosX + " " +
				  PlayerManager.Instance.PosY + " " +
				  PlayerManager.Instance.PosZ);
        Debug.Log(data.posX + " " + data.posY + " " + data.posZ);
        stream.Close();
        SaveScreenshot(path);
    }

    public void Continue()
    {
        FileInfo[] files = LoadAllSavefile();
        if (files != null)
        {
            Load(files[files.Length - 1].FullName);
        }
    }

    public void Load(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close();

            PlayerManager.Instance.SetPlayerData(data);
            ScreenManager.Instance.TriggerLoadingFadeOut("M0002", false);
        }
    }

    public FileInfo[] LoadAllSavefile()
    {
        /*string[] savePaths = Directory.GetFiles(savePath, "*" + FILE_EXTENSION);
        for(int i=0;i<savePaths.Length;i++)
        {
            Debug.Log(savePaths[i]);
        }*/

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
            return null;
        }
        else
        {
            DirectoryInfo di = new DirectoryInfo(savePath);
            FileInfo[] files = di.GetFiles("*" + FILE_EXTENSION).OrderBy(p => p.CreationTime).ToArray();
            //for(int i=0;i<files.Length;i++)
            //{
            //    Debug.Log(files[i].FullName);
            //}
            return files.Length > 0 ? files : null;
        }
    }

    void SaveScreenshot(string path)
    {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = path + ".png";
        System.IO.File.WriteAllBytes(filename, bytes);
    }
}