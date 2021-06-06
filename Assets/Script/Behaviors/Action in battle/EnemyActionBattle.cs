using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionBattle : MonoBehaviour {
	[SerializeField] private string targetsTag;
	[SerializeField] private AudioSource enemyAudioSource;
	private bool isActionStarted = false;
	private bool isAttack = false;
	GameObject target;
	private Vector3 targetPosition;
	private Vector3 startPosition;
	int count = 0;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}

	GameObject FindRandomTarget() {
		GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(targetsTag);
		if (possibleTargets.Length > 0) {
			int targetIndex = Random.Range(0, possibleTargets.Length);
			if (possibleTargets.Length == 1) {
				targetIndex = 0;
			}
			target = possibleTargets[targetIndex];
			return target;
		}
		else {
			return null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (BattleManager.instance.callTurn == true && BattleManager.instance.isGameOver == false && BattleManager.instance.isVictory == false && BattleManager.instance.enemyTurn == true) {
			if (BattleManager.instance.currentUnit.transform.position.x == this.gameObject.transform.position.x && isActionStarted == true) {
				if (isAttack == false) {
					targetPosition = new Vector3(target.transform.position.x + 2.0f, 
					                             target.transform.position.y, target.transform.position.z);
					if (this.transform.position == targetPosition) {
						Hit();
						isAttack = true;
					}
				}
				else {
					targetPosition = startPosition;
					if (this.transform.position == targetPosition) {
						BattleManager.instance.isPlayerSelectEnemy = false;
						BattleManager.instance.isUnitAction = false;
						BattleManager.instance.enemyTurn = false;
						if (BattleManager.instance.isFirstTurn == true) {
							Debug.Log("a");
							isActionStarted = false;
							BattleManager.instance.enemyTurn = false;
							BattleManager.instance.FristTurn();

                            isAttack = false;
						}
						else {
							isActionStarted = false;
							BattleManager.instance.enemyTurn = false;
							BattleManager.instance.nextTurn();
                            isAttack = false;
						}
					}
				}
				this.transform.position = Vector3.MoveTowards(transform.position, targetPosition, 500.0f * Time.deltaTime);
			}
		}
	}

	public void Action () {
		if (BattleManager.instance.enemyTurn==true) {
			target = FindRandomTarget();
			count++;
			Debug.Log(count);
			isActionStarted = true;
			//isAttack = false;
		}
	}

	private void Hit () {
		enemyAudioSource.clip = AudioManager.Instance.battleBgms[3];
        enemyAudioSource.Play();
		GenerateDamageText targetText = target.GetComponent<GenerateDamageText>();
		isAttack = true;
		targetText.ReceiveDamage((int)this.gameObject.GetComponent<EnemyStat>().enemy.stats.strength);
	}
}
