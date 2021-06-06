using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour {
	public EnemyUnit enemy;
	public int damageToHp = 0;
	public int durationTime = 0;
	public bool isHaveDeBuff = false;

	public void Init (int index) {
		enemy = new EnemyUnit(DataManager.Instance.EnemyList[index]);
	}
}
