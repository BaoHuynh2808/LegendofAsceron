using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectReward : MonoBehaviour {
    [SerializeField] private float experience;
	// Use this for initialization
	void Start () {
        GameObject battleManager = GameObject.Find("BattleManager");
        //battleManager.GetComponent<BattleManager>().enemyEncounter = this.gameObject;
	}
}
