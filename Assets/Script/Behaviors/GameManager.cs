using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public enum Level {
	Passive,
	Normal,
	Aggressive,
	Nightmare
}

public class GameManager:MonoBehaviour {
    public static GameManager instance = null;
	[SerializeField] private GameObject enemyPrefab;
	public int index;//store index of save file
    private Vector3 currentPlayerPosition = new Vector3();
	private bool isSceneMenu;
	public List<string> playerNameDestroy;
	public bool IsPause { get; set; }
	//check is player on map scene attack enemy 
	public bool isPlayerAttackEnemy;
	//check is enemy on map scene attack player
	public bool isEnemyAttackPlayer;
	public bool isBattleSceneAnimationLoaded;
	public bool isBossFight;


    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(gameObject);
        }
		IsPause = false;
        DontDestroyOnLoad(gameObject);
		index = PlayerPrefs.GetInt("c");
		isSceneMenu = true;
		isBattleSceneAnimationLoaded = false;
		isBossFight = false;
		playerNameDestroy = new List<string>();
    }

	private void Update()
	{
		if (IsPause)
		{
			Time.timeScale = 0;
		}
		else 
		{
			Time.timeScale = 1;
		}
	}
}
