using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFX : MonoBehaviour, ISelectHandler
{

    public void ClickButton()
    {
        AudioManager.Instance.PlayButtonSFX(1);
    }

    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.Instance.PlayButtonSFX(0);
    }
}
