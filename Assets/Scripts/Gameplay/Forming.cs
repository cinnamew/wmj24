using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Forming : MonoBehaviour
{
    public GameObject felting;
    public GameObject feltBase, hand;
    public Sprite baseSprite;
    public Sprite[] bearSprites, catSprites, rabbitSprites;
    public GameObject activeClickable, clickablePrefab;
    public float delay, life;
    public int count, stepCount;

    public IEnumerator FormingGameplay()
    {
        count = 0;
        hand.SetActive(false);
        felting.SetActive(true);

        while (!GameplayController.instance.isComplete)
        {
            if (count >= 4*stepCount)
            {
                GameplayController.instance.isComplete = true;
            }

            if(activeClickable == null)
            {
                yield return new WaitForSeconds(delay);
                StartCoroutine(FormButton());
            }

            if(Input.GetMouseButtonDown(0))
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

        GameplayController.instance.GameActive(2);
    }

    IEnumerator FormButton()
    {
        GenerateClickable();
        yield return new WaitForSeconds(life);
        ClearFormButton();
    }

    void GenerateClickable()
    {
        float xExtent, yExtent;
        xExtent = feltBase.GetComponent<CircleCollider2D>().bounds.extents.x;
        yExtent = feltBase.GetComponent<CircleCollider2D>().bounds.extents.y;

        activeClickable = Instantiate(clickablePrefab, new Vector3(Random.Range(-xExtent, xExtent), Random.Range(-yExtent, yExtent), -2), new Quaternion(0,0,0,0));
    }

    public void Clicked()
    {
        count++;
        if (count >= stepCount) feltBase.GetComponent<SpriteRenderer>().sprite = rabbitSprites[Math.Clamp(count/stepCount, 0, 2)];
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
        yield return new WaitForSeconds(.5f);
        hand.SetActive(false);
    }

}
