using UnityEngine;
using UnityEngine.UI;

public class UiPanelTemplate : MonoBehaviour
{
    public Button backButton;

    void Back()
    {
        backButton.onClick.Invoke();
    }

    private void FixedUpdate()
    {
        if (hInput.GetButton("Back"))
        {
            Back();
        }
        CustomAction();
    }

    protected virtual void CustomAction()
    {

    }
}
