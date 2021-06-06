using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransition : MonoBehaviour {

	public static MapTransition instance = null;
    [SerializeField]
    GameObject[] entrancePoints;
    [SerializeField]
    string[] entranceMapIds;
    [SerializeField]
    GameObject[] exitPoints;
    [SerializeField]
    string[] exitMapIds;
    [SerializeField]
    string mapId;
	public GameObject saveText;

	private void Awake()
	{
		if (instance == null) {
			instance = this;
		}
	}
	private void Start()
    {
        GoToMap();
        //Camera.main.GetComponent<RTS_Cam.RTS_Camera>().targetFollow
        //= PlayerManager.Instance.player.transform;
        //PlayerManager.Instance.player.GetComponent<MSCameraController>().cameras[0]._camera = Camera.main;
    }

    public void GoToMap()
    {
        //if (PlayerManager.Instance.CheckNewGame && mapId == "M0001")
        //{
        //    PlayerManager.Instance.player.transform.position = entrancePoints[0].transform.position;
        //    PlayerManager.Instance.CheckNewGame = false;
        //}
        //else
        //{
            for (int i=0; i< entranceMapIds.Length; i++)
            {
                if (PlayerManager.Instance.CurrentSceneID == entranceMapIds[i])
                {
                    PlayerManager.Instance.player.transform.position = entrancePoints[i].transform.position;
                    return;
                }
            }
        //}
    }
}
