using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectorMovement : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (BattleManager.instance.isPlayerSelectEnemy == true) {
			Destroy(this.gameObject);
		}
		if (BattleManager.instance.isTurnSkip == true) {
			Destroy(this.gameObject);
		}
	}

	private void OnDestroy() {
		BattleManager.instance.isTurnSkip = false;
	}
}
