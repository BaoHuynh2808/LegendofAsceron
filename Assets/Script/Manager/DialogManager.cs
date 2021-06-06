using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JsonDataClasses;
using Newtonsoft.Json;

public class DialogManager : MonoBehaviour
{

    public static DialogManager Instance { get; private set; }

    public List<DialogsJson> dialogList = new List<DialogsJson>();
    
    [SerializeField]
    TextMeshProUGUI charName;
    [SerializeField]
    TextMeshProUGUI charContext;
    [SerializeField]
    Image charFace;
    [SerializeField]
    Canvas dialogCanvas;

    int dialogCount = -1;
    DialogsJson runningDialogs = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        Init();
        DontDestroyOnLoad(this);
    }

    void Init()
    {
        var dialogsJson = Resources.Load<TextAsset>("JSON/dialogs");
        DialogsJson[] tempEnemyList = JsonConvert.DeserializeObject<DialogsJson[]>(dialogsJson.text);
        dialogList.AddRange(tempEnemyList);
    }

    public DialogsJson SearchDialogID(string id)
    {
        for (int i = 0; i < dialogList.Count; i++)
        {
            if (dialogList[i].id == id)
            {
                return dialogList[i];
            }
        }
        return null;
    }

    public void RunDialog(string id)
    {
        DialogsJson dialogs = SearchDialogID(id);
        if (dialogs != null)
        {
            if (runningDialogs == null)
            {
                dialogCount = 0;
                runningDialogs = dialogs;
                SetActive(true);
                ContinueDialog();
            }
            else
            {
                Debug.LogError("A dialog is currently running. Please wait!");
            }
        }
    }

    public void ContinueDialog()
    {
        Debug.Log("length: "+runningDialogs.dialogs.Length);
        if (dialogCount < runningDialogs.dialogs.Length)
        {
            Debug.Log(dialogCount);
            SetDialog(runningDialogs.dialogs[dialogCount].name,
                      runningDialogs.dialogs[dialogCount].context,
                      runningDialogs.dialogs[dialogCount].imgPath
                     );
            dialogCount++;
        }
        else 
        {
            dialogCount = -1;
            runningDialogs = null;
            SetActive(false);
        }
    }

    public bool IsDialogCanvasActive()
    {
        return dialogCanvas.gameObject.activeInHierarchy;
    }

    public void SetActive(bool isActive)
    {
        dialogCanvas.gameObject.SetActive(isActive);
    }

    void SetDialog(string _name, string _context, string _imgName)
    {
        SetCharName(_name);
        SetCharContext(_context);
        SetCharImage(_imgName);
    }

    void SetCharName(string _name)
    {
        charName.text = _name;
    }

    void SetCharContext(string _context)
    {
        charContext.text = _context;
    }

    void SetCharImage(string _imgName)
    {
        Sprite texture = Resources.Load<Sprite>( _imgName);
        if (texture)
        {
            charFace.sprite = texture;
        }
    }

}
