using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CharacterStats_Stats : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI fieldType;
    [SerializeField]
    private TextMeshProUGUI currentValue;
    [SerializeField]
    private TextMeshProUGUI changedValue;

    Color positiveColor;
    Color negativeColor;
	// Use this for initialization
	void Awake()
    {
        fieldType = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        currentValue = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        changedValue = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        positiveColor = new Color(0, 255, 0);
        negativeColor = new Color(255, 0, 0);
    }
	
    public void OnUpdate(float value, float modifiedValue)
    {
        string valueString = string.Empty;
        string modifiedString = string.Empty;
        if (fieldType.text.Contains("(EVA)") || fieldType.text.Contains("(CRIT)") || 
            fieldType.text.Contains("Resistance"))
        {
            valueString = (value).ToString("N2") + "%";
            modifiedString = (modifiedValue).ToString("N2") + "%";
        }
        else
        {
            valueString = ((int)value).ToString("N0");
            modifiedString = ((int)modifiedValue).ToString("N0");
        }
        currentValue.text = valueString;
        if(modifiedValue == 0 || (!modifiedString.Contains("%") && (int)modifiedValue == 0))
        {
            changedValue.text = "";
        }
        else if (modifiedValue > 0)
        {
            changedValue.text = "+" + modifiedString;
            changedValue.color = positiveColor;
        }
        else
        {
            changedValue.text = "-" + modifiedString;
            changedValue.color = negativeColor;
        }
    }
}
