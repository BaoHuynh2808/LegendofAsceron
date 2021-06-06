using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JsonDataClasses;
using Newtonsoft.Json;

public class InteractController : MonoBehaviour {

    public static InteractController Instance { get; private set; }

    public List<SelectionsJson> selectionList = new List<SelectionsJson>();

    [SerializeField]
    TextMeshProUGUI charName;
    [SerializeField]
    TextMeshProUGUI charContext;
    [SerializeField]
    Image charFace;
    [SerializeField]
    Canvas selectionCanvas;
    [SerializeField]
    Button talkButton;
    [SerializeField]
    Button questButton;
    [SerializeField]
    Button shopButton;
    [SerializeField]
    Button exitButton;

    string talkdialogId;
    string shopId;
    string questId;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Init();
        DontDestroyOnLoad(this);
    }

    void Init()
    {
        var selectionsJson = Resources.Load<TextAsset>("JSON/selections");
        SelectionsJson[] tempEnemyList = JsonConvert.DeserializeObject<SelectionsJson[]>(selectionsJson.text);
        selectionList.AddRange(tempEnemyList);
        talkdialogId = "";
        shopId = "";    
        questId = "";
    }

    public SelectionsJson SearchSelectionID(string id)
    {
        for (int i = 0; i < selectionList.Count; i++)
        {
            if (selectionList[i].id == id)
            {
                return selectionList[i];
            }
        }
        return null;
    }

    public void OnShowSelection(string id)
    {
        if (!selectionCanvas.gameObject.activeSelf)
        {
            SelectionsJson selection = SearchSelectionID(id);
            if (selection != null)
            {
                GameManager.instance.IsPause = true;

                talkButton.gameObject.SetActive(false);
                questButton.gameObject.SetActive(false);
                shopButton.gameObject.SetActive(false);

                charName.text = selection.selectionDialog.name;
                charContext.text = selection.selectionDialog.context;
                Sprite texture = Resources.Load<Sprite>(selection.selectionDialog.imgPath);
                if (texture)
                {
                    charFace.sprite = texture;
                }

                selectionCanvas.gameObject.SetActive(true);
                talkdialogId = selection.talkId[Random.Range(0, selection.talkId.Length)];
                shopId = selection.shopId;
                questId = selection.questId;

                for (int i = 0; i < selection.dialogType.Length; i++)
                {
                    if (selection.dialogType[i].ToLower() == "talk")
                    {
                        talkButton.gameObject.SetActive(true);
                    }
                    else if (selection.dialogType[i].ToLower() == "quest")
                    {
                        questButton.gameObject.SetActive(true);
                    }
                    else if (selection.dialogType[i].ToLower() == "shop")
                    {
                        shopButton.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                Debug.LogError("Can't find Selection Dialogs");
            }
        }
    }

    public void OnTalkClick()
    {
        DialogManager.Instance.RunDialog(talkdialogId);
        OnExitClick();
    }

    public void OnShopClick()
    {
        ShopManager.Instance.OnShow(shopId);
        OnExitClick();
    }

    public void OnQuestClick()
    {
        OnExitClick();
    }

    public void OnExitClick()
    {
        talkdialogId = "";
        shopId = "";
        questId = "";
        selectionCanvas.gameObject.SetActive(false);
        GameManager.instance.IsPause = false;
    }
}
