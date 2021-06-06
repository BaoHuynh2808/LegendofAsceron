using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlayer : MonoBehaviour {
    private GameObject currentPlayer;
    private GameObject actionsMenu, enemyUnitsMenu;

	private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
	}

    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        if (scene.name == "BattleScene") {
            this.actionsMenu = GameObject.Find("ActionsMenu");
            this.enemyUnitsMenu = GameObject.Find("EnemyUnitMenu");
        }
    }

    public void SelectCurrentPlayer (GameObject player,PlayerStat currentPlayerStat) {
        this.currentPlayer = player;
        this.actionsMenu.SetActive(true);
        this.enemyUnitsMenu.SetActive(false);
    }

    public void SelectAttack () {
        
        this.actionsMenu.SetActive(false);
        this.enemyUnitsMenu.SetActive(true);
    }

    public void PlayerAttackEnemy (GameObject enemy) {
        BattleManager.instance.SetPlayerSelectAttack();
        this.actionsMenu.SetActive(false);
        this.enemyUnitsMenu.SetActive(false);
        //this.currentPlayer.GetComponent<GetPlayerAction>().AttackTarget(enemy);
    }
}
