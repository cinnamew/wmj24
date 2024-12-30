using UnityEngine;
using UnityEngine.Audio;

public class Phone : Singleton<Phone>
{
    [SerializeField] int workNum;
    [SerializeField] int callNum = 1;

    [SerializeField] AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Call();
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

    public void Call()
    {
        audioSource.Play();
        audioSource.loop = true;
    }
}
