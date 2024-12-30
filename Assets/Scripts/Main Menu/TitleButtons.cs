using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//You'll need to set up stuff in the editor, but you shouldn't really need to touch this script :)
public class TitleButtons : MonoBehaviour
{
    [SerializeField] string firstScene;
    public float alphaThreshold = 0.1f;

    void Start()
    {
        if (this.GetComponent<Image>() != null) this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        PlayerPrefs.SetInt("death", 0);
        PlayerPrefs.SetInt("delusion", 0);
        SceneManager.LoadScene(firstScene);
    }

    public void About()
    {

    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("currScene", firstScene));
    }

    public void SwitchToScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
