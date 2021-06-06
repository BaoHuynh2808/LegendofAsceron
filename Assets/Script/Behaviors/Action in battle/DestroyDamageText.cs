using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDamageText : MonoBehaviour {
    [SerializeField] private float destroyTime;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, this.destroyTime);
	}

	private void OnDestroy()
	{
		if (BattleManager.instance.IsPlayerUseItem == true) {
			if (BattleManager.instance.isFirstTurn == true) {
				BattleManager.instance.FristTurn();
			}
			else {
				BattleManager.instance.nextTurn();
			}
		}
	}
}
