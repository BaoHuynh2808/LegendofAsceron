using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadgameScripts : UiPanelTemplate {

    public GameObject savefilesPrefabs;
    public GameObject viewContent;
    public Image thumbnail;

    // Use this for initialization
    void Start () {
        FileInfo[] files = SaveLoadManager.Instance.LoadAllSavefile();
        if (files != null)
        {
            int totalPref = files.Length;
            float y = -80f;
            for (int i = 0; i < totalPref; i++)
            {
                GameObject savefilesButton = Instantiate(savefilesPrefabs) as GameObject;
                savefilesButton.transform.SetParent(viewContent.transform, false);
                savefilesButton.name += " " + i;
                Vector3 newPos = savefilesButton.transform.localPosition;
                newPos.y = y - 125f * i;
                savefilesButton.transform.localPosition = newPos;
                Debug.Log(savefilesButton.name);
                Debug.Log(files[totalPref - 1 - i].FullName);
                savefilesButton.GetComponent<SavefilesInfo>().ParseFileInfo(
                    files[totalPref - 1 - i]);
                savefilesButton.GetComponent<SavefilesInfo>().thumbnail = thumbnail;
            }
            thumbnail.gameObject.SetActive(false);
        }
	}

    protected override void CustomAction()
    {
        base.CustomAction();
    }
}
