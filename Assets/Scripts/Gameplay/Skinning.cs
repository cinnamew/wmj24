using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Skinning : MonoBehaviour
{
    public GameObject skinning;
    public GameObject bunny;
    public Texture bunnyStart, bunnyEnd;

    public IEnumerator SkinningGameplay()
    {
        Texture2D textureArea = new Texture2D((int)bunny.GetComponent<SpriteRenderer>().size.x, (int)bunny.GetComponent<SpriteRenderer>().size.y);
        skinning.SetActive(true);

        while (!GameplayController.instance.isComplete)
        {
            if (Input.GetMouseButton(0))
            {
                textureArea.SetPixel((int)Camera.main.ViewportToScreenPoint(Input.mousePosition).x, (int)Camera.main.ViewportToScreenPoint(Input.mousePosition).y, new Color(0, 1, 0));
            }
            yield return null;
        }

        //bunny.GetComponent<SpriteRenderer>().sprite = bunnyEnd;
        skinning.SetActive(false);

        GameplayController.instance.GameActive();
    }

}
