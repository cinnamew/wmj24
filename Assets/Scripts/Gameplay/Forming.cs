using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Fungus;

public class Forming : MonoBehaviour
{
    public GameObject felting;
    public GameObject feltBase, hand;
    public Sprite baseSprite, handSprite;
    public Sprite[] changeSprites;
    public Sprite bloodyHand, glitchSprite;
    public GameObject activeClickable, clickablePrefab;
    public float delay, life;
    public int count, stepCount;

    public GameObject phone;
    public AnimationClip[] animations;
    public RuntimeAnimatorController[] animators;

    public IEnumerator FormingGameplay(Flowchart flowchart)
    {
        count = 0;
        hand.SetActive(false);
        felting.SetActive(true);

        if (SceneManager.GetSceneByName("Minigame4") == SceneManager.GetActiveScene())
        {
            feltBase.AddComponent<Animator>().runtimeAnimatorController = animators[0];
            feltBase.AddComponent<Animation>().clip = animations[0];
        }

        // phone.SetActive(true);
        // phone.GetComponent<Phone>().Call("domo");

        while (!GameplayController.instance.isComplete)
        {
            if (count >= 4 * stepCount)
            {
                GameplayController.instance.isComplete = true;
            }

            if (activeClickable == null)
            {
                yield return new WaitForSeconds(delay);
                FormButton();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.forward);
                if (hit.collider.gameObject == activeClickable)
                {
                    Clicked();
                }
            }

            yield return null;
        }

        //SceneManager.LoadScene(nextScene);
        flowchart.ExecuteBlock("end");
        //GameplayController.instance.GameActive();  //CHANGE TO NEXT SCENE
    }

    public void FormButton()
    {
        GenerateClickable();
        Destroy(activeClickable, life);
    }

    void GenerateClickable()
    {
        float xExtent, yExtent;
        xExtent = feltBase.GetComponent<CircleCollider2D>().bounds.extents.x;
        yExtent = feltBase.GetComponent<CircleCollider2D>().bounds.extents.y;

        activeClickable = Instantiate(clickablePrefab, new Vector3(Random.Range(-xExtent, xExtent), Random.Range(-yExtent, yExtent), -2), new Quaternion(0, 0, 0, 0));
    }

    public void Clicked()
    {
        count++;
        if (count >= stepCount)
        {
            if (SceneManager.GetSceneByName("Minigame4") == SceneManager.GetActiveScene())
            {
                feltBase.GetComponent<Animator>().runtimeAnimatorController = animators[Math.Clamp(count / stepCount, 0, 2)];
                feltBase.GetComponent<Animation>().clip = animations[Math.Clamp(count / stepCount, 0, 2)];
            }
            else feltBase.GetComponent<SpriteRenderer>().sprite = changeSprites[Math.Clamp(count / stepCount, 0, 2)];
        }
        StartCoroutine(ShowHand());
        ClearFormButton();
    }

    void ClearFormButton()
    {
        Destroy(activeClickable);
        activeClickable = null;
    }

    IEnumerator ShowHand()
    {
        hand.SetActive(true);
        hand.transform.position = activeClickable.transform.position;
        if (count == stepCount || count == stepCount * 2)
        {
            if (glitchSprite != null) feltBase.GetComponent<SpriteRenderer>().sprite = glitchSprite;
            hand.GetComponent<SpriteRenderer>().sprite = bloodyHand;
            yield return new WaitForSeconds(.3f);
            hand.GetComponent<SpriteRenderer>().sprite = handSprite;
            if (glitchSprite != null) feltBase.GetComponent<SpriteRenderer>().sprite = changeSprites[Math.Clamp(count / stepCount, 0, 2)];
        }
        yield return new WaitForSeconds(.5f);
        hand.SetActive(false);
    }

}
