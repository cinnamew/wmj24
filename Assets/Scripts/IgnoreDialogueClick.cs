using UnityEngine;
using UnityEngine.EventSystems;
using Fungus;

public class IgnoreDialogueClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool disabled = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }



    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (DialogueManager.Instance != null) DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.Disabled);
    }

    public void ChangeClick()
    {
        if (!disabled)
        {
            DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.Disabled);
            disabled = true;
        }
        else
        {
            disabled = false;
            DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.ClickAnywhere);
        }
        //print(disabled);
    }

    public void EnableClick()
    {
        DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.ClickAnywhere);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!disabled && DialogueManager.Instance != null) DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.ClickAnywhere);
    }
}
