using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Skinning : MonoBehaviour
{
    public GameObject skinning;
    public GameObject bunny;
    public Texture2D bunnyStart, bunnyEnd;
    Texture2D textureArea;
    Material mat;
    Sprite sprite;

    private void Start()
    {
        Vector2 origin = bunny.transform.position;
        Vector2 size = new Vector2(bunnyStart.width, bunnyStart.height);


        mat = new Material(Shader.Find("UI/Default"));
        //textureArea = Instantiate(bunnyStart);
        mat.mainTexture = bunnyStart;
        //sprite = Sprite.Create(textureArea, new Rect(origin.x, origin.y, size.x, size.y), bunny.GetComponent<RectTransform>().pivot);
        bunny.GetComponent<MeshRenderer>().material = mat;
        
    }

    public IEnumerator SkinningGameplay()
    {
        skinning.SetActive(true);

        Vector2 pos;
        int posX, posY;

        while (!GameplayController.instance.isComplete)
        {
            if (Input.GetMouseButton(0))
            {


                //RectTransformUtility.ScreenPointToLocalPointInRectangle(sprite.rect, Input.mousePosition, Camera.main, out pos);
                //posX = (int)pos.x;
                //posY = (int)pos.y;
                //Debug.Log(pos);
                //textureArea.SetPixel(posX, posY, bunnyEnd.GetPixel(posX, posY));
            }
            yield return null;
        }

        //bunny.GetComponent<SpriteRenderer>().sprite = bunnyEnd;
        skinning.SetActive(false);

        GameplayController.instance.GameActive();
    }

}
