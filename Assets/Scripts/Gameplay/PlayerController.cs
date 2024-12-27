using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(GameActive());
    }


    public IEnumerator GameActive()
    {

        while(true)
        {
            this.gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            yield return null;
        }

    }



}
