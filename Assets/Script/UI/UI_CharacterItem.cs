using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_CharacterItem : MonoBehaviour, ISelectHandler, IDeselectHandler {

    [SerializeField]
    private int index;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Image face;
    [SerializeField]
    private Slider hp;
    [SerializeField]
    private Slider mp;
    [SerializeField]
    private Slider exp;
    [SerializeField]
    private TextMeshProUGUI level;
    [SerializeField]
    private TextMeshProUGUI expText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void UpdateImage()
    {
        string charId = PlayerManager.Instance.PartyMemberID[index];
        PlayerCharacter member = PlayerManager.Instance.SearchCharacterById(charId);
        //Debug.Log(member.faceImgPath);
        face.sprite = Resources.Load<Sprite>(member.info.faceImgPath);
    }

    public void OnSelect(BaseEventData eventData)
    {
        UpdateImage();
        UpdatePlayerStat();
        animator.SetTrigger("select");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        animator.SetTrigger("deselect");
    }

    void UpdatePlayerStat()
    {
        string charId = PlayerManager.Instance.PartyMemberID[index];
        Debug.Log(charId);
        PlayerCharacter member = PlayerManager.Instance.SearchCharacterById(charId);
        Debug.Log(member.battleStats.hp + "/" + member.battleStats.maxHp);
        Debug.Log(member.info.faceImgPath);
        hp.value = member.battleStats.hp / member.battleStats.maxHp;
        mp.value = member.battleStats.mp / member.battleStats.maxMp;
        level.text = member.level.ToString();
        expText.text = "To Next Level: " + (Expression.GetExpExpression(member.level+1) - member.experience).ToString();
        exp.value = (Expression.GetExpExpression(member.level + 1) - member.experience) / Expression.GetExpExpression(member.level + 1);
    }
}
