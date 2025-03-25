using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skinning : MonoBehaviour
{
    public GameObject skinning;
    public Camera cam;
    public SpriteMask mask;
    public GameObject razor, razorPrefab, linePrefab;
    public GameObject skinObj;
    public Texture2D skinStart, skinEnd;
    
    public float completeThreshold;

    public IEnumerator SkinningGameplay()
    {
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

    IEnumerator CheckForComplete(int xNum, int yNum, GameObject go)
    {
        int num = 0;
        Vector2 size = skinObj.GetComponent<SpriteRenderer>().size;
        Vector3 offset = new Vector2(0, 1);
        
        for(int x = 0; x < xNum; x++)
        {
            for(int y = 0; y < yNum; y++)
            {
                RaycastHit hit;
                
                if(Physics.Raycast(new Vector3(x*(size.x/xNum)-(size.x/2), y*(size.y/yNum)-(size.y/2), -20)+offset, Vector3.forward, out hit, Mathf.Infinity))
                {
                    if(hit.collider.gameObject == go) num++;
                }

                // RaycastHit2D hit = Physics2D.Raycast(new Vector2(x*(size.x/xNum)-(size.x/2), y*(size.y/yNum)-(size.y/2))+offset, Vector2.zero);
                // Debug.DrawRay(hit.point, Vector2.down, Color.blue);
                
                // if(hit.collider != null) 
                // {
                //     Debug.Log(hit.collider.gameObject.name);
                //     if(hit.collider.gameObject == skinObj) num++;
                // }
            }
        }
        
        if(num >= xNum*yNum*completeThreshold)
        {
            Debug.Log("Done");
        }
        else 
        {
            yield return new WaitForSeconds(.5f);
            StartCoroutine(CheckForComplete(xNum, yNum, go));       
        }
    }
}

