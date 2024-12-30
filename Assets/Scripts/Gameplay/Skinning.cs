using System.Collections;
using UnityEngine;

public class Skinning : MonoBehaviour
{
    
    public IEnumerator SkinningGameplay()
    {


        while (!GameplayController.instance.isComplete)
        {
            yield return null;
        }
    }



}
