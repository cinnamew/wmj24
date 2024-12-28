using UnityEngine;

//but only for the ui i'm just hardcoding
public class Stay : Singleton<Stay>
{
    [SerializeField] GameObject settingsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
    }


    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }
}
