using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyOnSelect : MonoBehaviour, ISelectHandler
{
    public NewGameUIScript script;
    public int index;
    public void OnSelect(BaseEventData eventData)
    {
        script.UpdateInfoText(index);
    }
}
