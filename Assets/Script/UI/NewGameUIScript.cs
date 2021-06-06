using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGameUIScript : UiPanelTemplate {

    public TextMeshProUGUI infoText;
    protected int difficulty;
	[SerializeField] private GameObject prologuePanel;

	// Use this for initialization
	void Start () {
        difficulty = -1;
	}

    public void UpdateInfoText(int index)
    {
        difficulty = index;
        float data = (float)((index == 0) ? 1.25 : ((index == 1) ? 1.00 : ((index == 2) ? 0.75 : 0.5)));
        int timeScale = ((index == 0) ? 20 : ((index == 1) ? 15 : ((index == 2) ? 10 : 5)));
        infoText.text = string.Format("Experience Modifier: {0}.\nTime Limit: {1} seconds.", data, timeScale);
    }

    public void AcceptNewGame()
    {
        if (difficulty != -1)
        {
            PlayerManager.Instance.Initialize(difficulty);
			//ScreenManager.Instance.TriggerLoadingFadeOut(
			//MapManager.Instance.NewGameSceneID, true);
			prologuePanel.SetActive(true);
			prologuePanel.GetComponent<Animator>().SetTrigger("rolling");
            PlayerManager.Instance.SetPlayerPos();
        }
    }

    protected override void CustomAction()
    {
        base.CustomAction();
    }
}
