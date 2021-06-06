using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewGame : MonoBehaviour {
    public void ClickOnNewGame()
    {
        PlayerManager.Instance.Initialize(1);
        ScreenManager.Instance.TriggerLoadingFadeOut(
            MapManager.Instance.NewGameSceneID, true);
        PlayerManager.Instance.SetPlayerPos();
    }
}
