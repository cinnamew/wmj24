using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{

    public GameObject gameStateText;


    public void SetUI(string title)
    {
        gameStateText.GetComponent<Text>().text = title;
    }

}
