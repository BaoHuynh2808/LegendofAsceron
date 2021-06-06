using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectOnInput : MonoBehaviour {
	public EventSystem eventSystem;
	public GameObject selectedObject;
	private bool buttonSelected;

	// Update is called once per frame
	void Update () {
        if (hInput.GetAxis("VerticalAxis") != 0.0f && buttonSelected == false) {
			//if (BattleManager.instance.isPlayerSelectAttack() == true) {
			//	GameObject[] chooseEnemyButton = GameObject.FindGameObjectsWithTag("ChooseEnemyButton");
			//	selectedObject = chooseEnemyButton[0];
			//}
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}
	}

	private void OnDisable() {
		buttonSelected = false;
	}
}
