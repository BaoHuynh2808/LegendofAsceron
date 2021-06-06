using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSceneScripts : MonoBehaviour {

    public GameObject optionsMenu;
	public bool isProloguePanel;

    public void OnFinishAnimation()
    {
		if (isProloguePanel == true) {
			ScreenManager.Instance.TriggerLoadingFadeOut(
            MapManager.Instance.NewGameSceneID, true);
		}
		else {
			optionsMenu.SetActive(true);
            gameObject.SetActive(false);
		}
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return)) {
			OnFinishAnimation();
		}
	}
}
