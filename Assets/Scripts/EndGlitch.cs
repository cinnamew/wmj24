using UnityEngine;
using System.Collections;

public class EndGlitch : MonoBehaviour
{
    [SerializeField] Sprite day;
    [SerializeField] Sprite horror;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Glitch());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Glitch()
    {
        print("yay");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = horror;
        float a = UnityEngine.Random.Range(0.01f, 0.2f);
        if (UnityEngine.Random.Range(0, 4) == 1)
        {
            //long
            a = UnityEngine.Random.Range(0.4f, 0.6f);
            print(a);
        }
        yield return new WaitForSeconds(a);

        print("wah");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = day;
        float num = UnityEngine.Random.Range(0.1f, 3f);
        //print(num);
        yield return new WaitForSeconds(num);
        StartCoroutine(Glitch());
    }
}
