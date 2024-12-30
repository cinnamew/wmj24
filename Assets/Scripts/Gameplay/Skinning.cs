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
            if(Input.GetMouseButtonDown(0)) GameplayController.instance.isComplete = true;
            yield return null;
        }

        bunny.GetComponent<SpriteRenderer>().sprite = bunnyEnd;
        skinning.SetActive(false);

        GameplayController.instance.GameActive();
    }

}
