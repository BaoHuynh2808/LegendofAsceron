using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CharacterStats_Attributes : MonoBehaviour {
    [SerializeField]
    private UI_CharacterStats parent;
    [SerializeField]
    private TextMeshProUGUI valueType;
    [SerializeField]
    private TextMeshProUGUI currentValue;
    [SerializeField]
    private TextMeshProUGUI changedValue;
    [SerializeField]
    public Button addButton;
    [SerializeField]
    public Button subtractButton;

    int add;

    void Start()
    {
        add = 0;
        parent = GetComponentInParent<UI_CharacterStats>();
        valueType = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        currentValue = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        changedValue = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        subtractButton = transform.GetChild(3).GetComponent<Button>();
        addButton = transform.GetChild(4).GetComponent<Button>();
        currentValue.text = "";
        changedValue.text = "";
    }

    public void OnShow(int value, int attribute)
    {
        currentValue.text = value.ToString();
        if (attribute == 0)
        {
            changedValue.gameObject.SetActive(false);
            subtractButton.gameObject.SetActive(false);
            addButton.gameObject.SetActive(false);
        }
        else
        {
            changedValue.gameObject.SetActive(true);
            subtractButton.gameObject.SetActive(true);
            addButton.gameObject.SetActive(true);
            changedValue.text = "";
        }
    }

    public void RefreshAll()
    {
        add = 0;
        changedValue.text = "";
    }

    public void OnButtonClick(ref int attributes, bool isUp)
    {
        if (isUp && attributes > 0)
        {
            add++;
            attributes--;
            changedValue.text = "+" + add;
        }
        else if (!isUp && add > 0)
        {
            add--;
            attributes++;
            if (add != 0)
            {
                changedValue.text = "+" + add;
            }
            else
            {
                changedValue.text = "";
            }
        }
        parent.UpdateAvailablePoints();
        parent.PreviewStatsFromAttributes();
    }

    public int GetAddValue()
    {
        return add;
    }
}
