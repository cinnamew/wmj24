using System.Collections;
using UnityEngine;

public class Skinning : MonoBehaviour
{
    public GameObject skinning;
    public GameObject bunny;
    public Sprite bunnyStart, bunnyEnd;

    public IEnumerator SkinningGameplay()
    {
        skinning.SetActive(true);

        while (!GameplayController.instance.isComplete)
        {
            yield return null;
        }

        bunny.GetComponent<SpriteRenderer>().sprite = bunnyEnd;
    }



}
