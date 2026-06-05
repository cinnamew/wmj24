using UnityEngine;
using UnityEngine.EventSystems;

public class SkinningOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseInside;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("mouse in");
        mouseInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("mouse out");
        mouseInside = false;
    }
}
