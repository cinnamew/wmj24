using System.Collections;
using UnityEngine;

public class Stitching : MonoBehaviour
{
    

    public IEnumerator StitchingGameplay()
    {


        while (!GameplayController.instance.isComplete)
        {
            yield return null;
        }
    }



}
