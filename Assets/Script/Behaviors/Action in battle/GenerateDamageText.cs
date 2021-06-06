using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateDamageText : MonoBehaviour {
	[SerializeField] private GameObject damageText;
	[SerializeField] private float damageTextOffset;
	private GameObject damageTextObject;
	[SerializeField] private AudioSource unitAudio;
	private float maxHealth;
	private float currentHealth;

	public void RecoverHealth (int recoverPoint) {
		GameObject canvas = GameObject.Find("Canvas");
		damageTextObject = Instantiate(this.damageText) as GameObject;
		damageTextObject.transform.SetParent(canvas.transform, false);
		damageTextObject.GetComponent<TMPro.TextMeshProUGUI>().color = Color.green;
		damageTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "" + recoverPoint;
		damageTextObject.transform.position = new Vector3(transform.position.x + damageTextOffset,
														  transform.position.y + 1.0f, transform.position.z);
	}

	public void ReceiveDamage(int damage) {
		if (gameObject.tag == "PlayerUnit") {
			currentHealth = this.gameObject.GetComponent<PlayerStat>().player.battleStats.hp;
			currentHealth -= damage;
			this.gameObject.GetComponent<PlayerStat>().player.battleStats.hp = currentHealth;
		}
		else {
			currentHealth = this.gameObject.GetComponent<EnemyStat>().enemy.stats.hp;
			currentHealth -= damage;
			this.gameObject.GetComponent<EnemyStat>().enemy.stats.hp = currentHealth;
		}

        GameObject canvas = GameObject.Find("Canvas");
        damageTextObject = Instantiate(this.damageText) as GameObject;
		damageTextObject.transform.SetParent(canvas.transform, false);
        damageTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "" + damage;

		damageTextObject.transform.position = new Vector3(transform.position.x + damageTextOffset,
		                                                  transform.position.y + 1.0f,transform.position.z);
		if (currentHealth <= 0.0f) {
			unitAudio.clip = AudioManager.Instance.battleBgms[4];
			unitAudio.Play();
			if (this.gameObject.tag == "Enemy") {
				//BattleManager.instance.enemyList.Remove(unit);
				this.gameObject.tag = "DeadUnit";
				BattleManager.instance.isEnemyDead[BattleManager.instance.enemySelectedPositionIndex] = true;
				this.gameObject.SetActive(false);
			}
			else {
				this.gameObject.tag = "DeadUnit";
			}
        }
    }
}
