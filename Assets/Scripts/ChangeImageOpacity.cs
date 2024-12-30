using UnityEngine;
using UnityEngine.UI;

public class ChangeImageOpacity : MonoBehaviour
{
    [SerializeField] Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateOpacity(float f)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, f);
    }
}
