using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCBehavior : MonoBehaviour, IPointerClickHandler {

    [SerializeField]
    string id;
    [SerializeField]
    string selectionId;

    /*public void OnMouseDown()
    {
        float distance = Vector3.Distance(PlayerManager.Instance.player.transform.position, transform.position);
        Debug.Log(distance);
        if (distance <= 3f && !EventSystem.current.IsPointerOverGameObject())
        {
            OnInteract();
        }
    }*/

    public void OnInteract()
    {
        transform.LookAt(PlayerManager.Instance.player.transform);
        InteractController.Instance.OnShowSelection(selectionId);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float distance = Vector3.Distance(PlayerManager.Instance.player.transform.position, transform.position);
        Debug.Log(distance);
        if (distance <= 3f)
        {
            OnInteract();
        }
    }
}
