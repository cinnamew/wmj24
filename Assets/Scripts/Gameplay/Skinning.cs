using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skinning : MonoBehaviour
{
    public GameObject skinning;
    public Camera cam;
    public SpriteMask mask;
    public GameObject razor, razorPrefab, linePrefab;
    public GameObject skinObj;
    public SpriteRenderer skinStart, skinEnd;
    [SerializeField] Sprite skinStartGlitch, skinEndGlitch;
    private Sprite beforeTop, beforeBot;
    
    public float completeThreshold;
    public GameObject phone;
    public GameObject dialogue;

    [SerializeField] SkinningOutline outline;

    private int numGlitches = 0;
    [SerializeField] int maxGlitches = 3;

    private RenderTexture render;
    private Texture2D texture;
    

    public void Start()
    {
        if (skinStart != null)
        {
            beforeTop = skinStart.sprite;
            beforeBot = skinEnd.sprite;
        }
        
    }

    public IEnumerator SkinningGameplay(Flowchart flowchart)
    {
        if (SceneManager.GetSceneByName("Minigame3") != SceneManager.GetActiveScene())
        {
            Debug.Log("phone active");
            phone.SetActive(true);
            phone.GetComponent<Phone>().Call("domo");
        }

        while (dialogue.activeSelf || phone.activeSelf) {
            yield return null;
        }
        Debug.Log("dialogue done");
        // StartCoroutine(Glitch());

        razor = Instantiate(razorPrefab, new Vector3(0,0,-3), new Quaternion(0,0,0,0));
        skinning.SetActive(true);
        GameObject lineObj = Instantiate(linePrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        LineRenderer line = lineObj.GetComponent<LineRenderer>();
        MeshCollider meshCollider = lineObj.GetComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        line.positionCount = 0;

        StartCoroutine(CheckForComplete(7, 5, lineObj));

        while (!GameplayController.instance.isComplete)
        {
            Vector3 position = razor.transform.position;
            position.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount-1, position);

            AssignMask();

            line.BakeMesh(mesh, true);
            meshCollider.sharedMesh = mesh;

            yield return null;
        }

        skinning.SetActive(false);
        Destroy(razor);

        //GameplayController.instance.GameActive();
        GameplayController.instance.GameActive(1);  //CHANGE TO NEXT SCENE
    }

    public IEnumerator Glitch()
    {
        if (skinStartGlitch == null || skinEndGlitch == null) yield break;

        

        skinStart.sprite = skinStartGlitch;
        skinEnd.sprite = skinEndGlitch;

        yield return new WaitForSeconds(0.3f);

        skinStart.sprite = beforeTop;
        skinEnd.sprite = beforeBot;

        yield return new WaitForSeconds(1.5f);
    }

    void AssignMask()
    {
        int width = Screen.width, height = Screen.height;
        if (width == 0 || height == 0) return;

        if (render == null)
        {
            render  = new RenderTexture(width, height, 24);
            texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        }

        Rect rect = new Rect(0, 0, width, height);
        cam.targetTexture = render;
        cam.Render();

        var prev = RenderTexture.active;
        RenderTexture.active = render;
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();
        // cam.targetTexture = null;
        RenderTexture.active = prev;

        if (mask.sprite != null) Destroy(mask.sprite);
        Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), height / 20f);
        sprite.name = "line";
        mask.sprite = sprite;
    }

    IEnumerator CheckForComplete(int xNum, int yNum, GameObject go)
    {
        PolygonCollider2D oc = outline.GetComponent<PolygonCollider2D>();
        Bounds b = oc.bounds;

        int inside = 0;
        int num = 0;

        for (int x = 0; x < xNum; x++)
        {
            for (int y = 0; y < yNum; y++)
            {
                Vector2 p = new Vector2(
                    b.min.x + (x + 0.5f) * b.size.x / xNum,
                    b.min.y + (y + 0.5f) * b.size.y / yNum);

                if (!oc.OverlapPoint(p)) continue; 
                inside++;

                RaycastHit hit;
                if (Physics.Raycast(new Vector3(p.x, p.y, -20), Vector3.forward, out hit, Mathf.Infinity)
                    && hit.collider.gameObject == go)
                    num++;
            }
        }

        float completion = inside > 0 ? (float)num / inside : 0f;
        Debug.Log(completion + " vs " + completeThreshold);

        float a = UnityEngine.Random.Range(0f, 2f);
        if (completion >= completeThreshold / 2 && a <= 1 && numGlitches < maxGlitches)
        {
            StartCoroutine(Glitch());
            numGlitches++;
        }

        if (completion >= completeThreshold)
        {
            Debug.Log("Done");
            GameplayController.instance.isComplete = true;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            StartCoroutine(CheckForComplete(xNum, yNum, go));
        }
    }
}

