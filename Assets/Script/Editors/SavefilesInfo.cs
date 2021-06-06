using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SavefilesInfo : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{

    private FileInfo fileInfo;
    public Text eventName;
    public Text location;
    public Text date;
    public Text saveType;
    public Text playTime;
    public Image thumbnail;

    public void ParseFileInfo(FileInfo info)
    {
        fileInfo = info;
        OnUpdate();
    }

	void OnUpdate()
    {
        DateTime time = fileInfo.CreationTime;
        date.text = time.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        string[] nameSplit = fileInfo.Name.Split('_');
        if(nameSplit[0] == "Autosave")
        {
            saveType.text = "Autosave";
        }
        else
        {
            saveType.text = "Manual Save";
        }
        eventName.text = (nameSplit[1] == "0")? "Passive" : ((nameSplit[1] == "1") ? "Normal" : (nameSplit[1] == "2") ? "Aggressive" : "Hell") ;
        playTime.text = nameSplit[2].Substring(0, 4) + ":" 
            + nameSplit[2].Substring(4,2) + ":" 
            + nameSplit[2].Substring(6,2);
        Debug.Log(date.text);
    }

    public void OnClick()
    {
        Debug.Log(fileInfo.FullName);
        SaveLoadManager.Instance.Load(fileInfo.FullName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuff
        if (!thumbnail.IsActive())
        {
            thumbnail.gameObject.SetActive(true);
            ReadPNG();
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (thumbnail.IsActive())
        {
            thumbnail.gameObject.SetActive(false);
        }
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        if (!thumbnail.IsActive())
        {
            thumbnail.gameObject.SetActive(true);
            ReadPNG();
        }
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        if (thumbnail.IsActive())
        {
            thumbnail.gameObject.SetActive(false);
        }
    }

    private void ReadPNG()
    {
        string filePath = fileInfo.FullName.Remove(fileInfo.FullName.Length - 4) + ".png";
        Texture2D tex = null;
        byte[] fileData;
        if(File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2, TextureFormat.BGRA32, false);
            tex.LoadImage(fileData);
            Rect rect = new Rect(0, 0, tex.width, tex.height);
            thumbnail.sprite = Sprite.Create(tex, rect, new Vector2(0, 0), 1);
        }
    }
}
