using System.Collections;
using UnityEngine;

public class Stitching : MonoBehaviour
{
    public GameObject stitching;

    public IEnumerator StitchingGameplay()
    {
        stitching.SetActive(true);

        while (!GameplayController.instance.isComplete)
        {
            yield return null;
        }
    }



}
