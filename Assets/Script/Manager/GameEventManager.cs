using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{

    public static GameEventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        //Init();
        DontDestroyOnLoad(this);
    }

    void Init()
    {
        //DialogManager.Instance.RunDialog("D0001");
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogManager.Instance.IsDialogCanvasActive())
        {
            if (hInput.GetButtonDown("Interact") || Input.GetMouseButtonDown(0))
            {
                DialogManager.Instance.ContinueDialog();
            }
        }
    }

    void InvokeEvent()
    {

    }

}
