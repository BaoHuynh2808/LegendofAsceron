using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStartEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerManager.Instance.player.transform.position =
            new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
