using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEmeny : MonoBehaviour {
    public GameObject enemyMenuItem;

	void OnDestroy() {
        Destroy(this.enemyMenuItem);
	}
}
