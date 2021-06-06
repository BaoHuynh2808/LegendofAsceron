using UnityEngine;
using UnityEngine.UI;

public class UpdateVictoryPanelUI : MonoBehaviour {
	public static UpdateVictoryPanelUI instance = null;
	[SerializeField] private Text gilPointText;
	[SerializeField] private Text expPointText;
	[SerializeField] private GameObject player1Panel;
	[SerializeField] private Text player1NameText;
	[SerializeField] private Text player1LvText;
	[SerializeField] private Text player1ExpPointText;
	[SerializeField] private GameObject player2Panel;
	[SerializeField] private Text player2NameText;
	[SerializeField] private Text player2LvText;
	[SerializeField] private Text player2ExpPointText;
	[SerializeField] private GameObject player3Panel;
	[SerializeField] private Text player3NameText;
	[SerializeField] private Text player3LvText;
	[SerializeField] private Text player3ExpPointText;
	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	public void UpdateGilPoint (int gilPoint) {
		gilPointText.text = "" + gilPoint;
	}

	public void UpdateExpPoint (int expPoint) {
		expPointText.text = "" + expPoint;
	}

	public void UpdatePLayer1Lv (string player1Name, int player1Lv, int player1ExpPoint, int player1ExpPointToLvUp) {
		player1Panel.SetActive(true);
		player1NameText.text = "" + player1Name;
		player1LvText.text = "Lv " + player1Lv;
		player1ExpPointText.text = "" + player1ExpPoint + "/" + player1ExpPointToLvUp;
	}

	public void UpdatePlayer2Lv(string player2Name, int player2Lv, int player2ExpPoint, int player2ExpPointToLvUp) {
		player2Panel.SetActive(true);
		player2NameText.text = "" + player2Name;
		player2LvText.text = "Lv " + player2Lv;
		player2ExpPointText.text = "" + player2ExpPoint + "/" + player2ExpPointToLvUp;
	}
    
	public void UpdatePlayer3Lv(string player3Name, int player3Lv, int player3ExpPoint, int player3ExpPointToLvUp) {
		player3Panel.SetActive(true);
		player3NameText.text = "" + player3Name;
		player2LvText.text = "Lv " + player3Lv;
		player3ExpPointText.text = "" + player3ExpPoint + "/" + player3ExpPointToLvUp;
	}
}
