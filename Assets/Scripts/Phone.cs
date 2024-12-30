using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Phone : Singleton<Phone>
{
    [SerializeField] int workNum;
    [SerializeField] int callNum = 1;

    [SerializeField] TMP_Text name;

    [SerializeField] AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Call("domo");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickUpCall()
    {
        DialogueManager.Instance.RunBlock("w" + workNum + "c" + callNum);
        callNum++;
    }

    public void Call(string a)
    {
        audioSource.Play();
        audioSource.loop = true;
        if (a == "domo") a = "domomo";
        name.text = a;
    }
}
