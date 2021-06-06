using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour {
    public GameObject owner;
    public void Hit (GameObject target) {
        PlayerStat ownerStat = this.owner.GetComponent<PlayerStat>();
        PlayerStat targetStat = target.GetComponent<PlayerStat>();
        //targetStat.ReceiveDamage(ownerStat.attack);
    }
}
