using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour {

    public Animator anim;
    public Image loadingScreenArtwork;
    public Sprite[] artworkSprites;

    public int PreviousScene { get; set; }

    private bool isAvailable = true;
    private bool isLoading = false;
    private bool isTransitFromMap = false;
    private bool isBattle = false;

    private float countTimer = 5;
    private float intervalTime = 5;

    private static ScreenManager _instance;
    private string level;

    public static ScreenManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
            PreviousScene = 0;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        if (isLoading)
        {
            if (countTimer < intervalTime)
            {
                countTimer += Time.deltaTime;
            }
            else
            {
                countTimer = 0;
                intervalTime = Random.Range(5f, 10f);
                int randomSpriteIndex = Random.Range(0, artworkSprites.Length);
                loadingScreenArtwork.sprite = artworkSprites[randomSpriteIndex];
            }
        }
    }

    public void TriggerLoadingFadeOut(string screenIndex, bool transitFromMap)
    {
        if (isAvailable)
        {
            anim.SetTrigger("LoadingFadeOut");
            level = screenIndex;
            isLoading = true;
            isAvailable = false;
            isTransitFromMap = transitFromMap;
        }
    }

    void TriggerIdle()
    {
        anim.SetTrigger("Idle");
        isLoading = false;
        isBattle = false;
        isTransitFromMap = false;
        isAvailable = true;
    }

    public void TriggerBattleFadeOut()
    {
        if (isAvailable)
        {
            anim.SetTrigger("BattleFadeOut");
            level = MapManager.Instance.BattleSceneID;
            isTransitFromMap = false;
            isBattle = true;
            isAvailable = false;
            PlayerManager.Instance.SetPlayerXYZ(
                PlayerManager.Instance.player.transform.position.x,
                PlayerManager.Instance.player.transform.position.y,
                PlayerManager.Instance.player.transform.position.z);
            Debug.Log(PlayerManager.Instance.player.transform.position);
        }
    }

    void OnLoadComplete(string trigger)
    {
        PreviousScene = SceneManager.GetActiveScene().buildIndex;
        if(!isBattle)
        {
            PlayerManager.Instance.UpdateCurrentSceneID(level);
        }
        StartCoroutine(LoadAsynchronously(level, trigger));
    }

    IEnumerator LoadAsynchronously(string sceneID, string trigger)
    {
        int loadingScene = MapManager.Instance.mapList[sceneID].sceneId;
		AsyncOperation operation = SceneManager.LoadSceneAsync(loadingScene);
        while (!operation.isDone)
        {
            yield return null;
        }
		anim.SetTrigger(trigger);
        if (level == MapManager.Instance.BattleSceneID)
        {
            AudioManager.Instance.ChangeBgm(AudioManager.Instance.battleBgms[0]);
        }
        else
        {
            AudioManager.Instance.ChangeBgm(AudioManager.Instance.normalBgms[1]);
		}
        if (PlayerManager.Instance.CheckNewGame == false)
        {
			Debug.Log(PlayerManager.Instance.PosX +" "+ 
			          PlayerManager.Instance.PosY +" "+ PlayerManager.Instance.PosZ);
			PlayerManager.Instance.SetPlayerPos();
			Debug.Log(PlayerManager.Instance.player.transform.position.x +" "+
				PlayerManager.Instance.player.transform.position.y +" "+
			          PlayerManager.Instance.player.transform.position.z);
        }
        else
        {
			PlayerManager.Instance.SetPlayerXYZ(48f, 6f, 287.6f);
			PlayerManager.Instance.SetPlayerPos();
            PlayerManager.Instance.CheckNewGame = false;
        }
        Debug.Log(PlayerManager.Instance.player.transform.position);
    }
    
}
