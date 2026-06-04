using UnityEngine;

public class SkinningOutline : MonoBehaviour
{
    public bool mouseInside;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("mouse in");
        mouseInside = true;
    }

    private void OnTriggerExit(Collider other) { mouseInside = false; }

    public bool getMouseInside() { return mouseInside; }
    
}
