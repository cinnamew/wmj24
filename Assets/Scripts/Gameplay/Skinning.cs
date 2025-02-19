using System.Collections;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class Skinning : MonoBehaviour
{
    public GameObject skinning;
    public Camera cam;
    public SpriteMask mask;
    public GameObject razor, razorPrefab, linePrefab;
    public GameObject skinObj;
    public Texture2D skinStart, skinEnd;

    private void Start()
    {
        razor = Instantiate(razorPrefab, new Vector3(0,0,-3), new Quaternion(0,0,0,0));

    }

    public IEnumerator SkinningGameplay()
    {
        skinning.SetActive(true);
        GameObject lineObj = Instantiate(linePrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        LineRenderer line = lineObj.GetComponent<LineRenderer>();
        line.positionCount = 0;

        while (!GameplayController.instance.isComplete)
        {
            Vector3 position = razor.transform.position;
            position.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount-1, position);
            AssignMask();

            yield return null;
        }

        skinning.SetActive(false);

        GameplayController.instance.GameActive();
    }

    void AssignMask()
    {
        int height = Screen.height;
        int width = Screen.width;
        int depth = 10;

        RenderTexture render = new RenderTexture(width, height, depth);
        Rect rect = new Rect(0, 0, width, height);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        cam.targetTexture = render;
        cam.Render();

        RenderTexture currentRender = RenderTexture.active;
        RenderTexture.active = render;
        texture.ReadPixels(rect, 0,0);
        texture.Apply();

        cam.targetTexture = null;
        RenderTexture.active = currentRender;
        Destroy(render);

        Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), height/20);
        sprite.name = "line";
        mask.sprite = sprite;
        
    }
}

