using UnityEngine;

public class PlayerStat : MonoBehaviour {
	public PlayerCharacter player;

	public void Init (int index) {
		player = PlayerManager.Instance.Characters[index];
	}
}
