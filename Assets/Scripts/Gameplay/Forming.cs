using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Forming : MonoBehaviour
{
    public GameObject feltBase, hand;
    public Sprite baseSprite;
    public Sprite[] bearSprites, catSprites, rabbitSprites;
    public GameObject activeClickable, clickablePrefab;
    public float delay, life;
    public int count;

    public IEnumerator FormingGameplay()
    {
        count = 0;

        while (!GameplayController.instance.isComplete)
        {

            if (count >= 4)
            {
                GameplayController.instance.isComplete = true;
            }

            if(activeClickable == null)
            {
                yield return new WaitForSeconds(delay);
                GenerateClickable();
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
    }

    IEnumerator FormButton()
    {
        yield return new WaitForSeconds(life);
        ClearFormButton();
    }

    void GenerateClickable()
    {
        float xExtent, yExtent;
        xExtent = feltBase.GetComponent<CircleCollider2D>().bounds.extents.x;
        yExtent = feltBase.GetComponent<CircleCollider2D>().bounds.extents.y;

        activeClickable = Instantiate(clickablePrefab, new Vector3(Random.Range(-xExtent, xExtent), Random.Range(-yExtent, yExtent), -1), new Quaternion(0,0,0,0));
    }

    public void Clicked()
    {
        count++;
        feltBase.GetComponent<SpriteRenderer>().sprite = rabbitSprites[count-1];
        ClearFormButton();
        StopCoroutine(FormButton());
    }

    void ClearFormButton()
    {
        Destroy(activeClickable);
        activeClickable = null;
    }

}
