using UnityEngine;
using UnityEngine.EventSystems;

public class SkinningOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseInside;
    [SerializeField] private Collider2D outlineCollider;

    void Start()
    {
        outlineCollider = GetComponent<Collider2D>();
    }

    //trying to use this inside
    public bool IsInside(Vector2 worldPoint)
    {
        return outlineCollider.OverlapPoint(worldPoint);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("mouse in");
        mouseInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("mouse out");
        mouseInside = false;
    }
}
