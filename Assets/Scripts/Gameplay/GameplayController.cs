using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        else instance = this;

    }

    private GameplayUI ui;


    public enum GameState
    {
        SKINNING,
        TANNING,
        FLESHING,
        STITCHING,
        FORMING,
        MOUNTING
    }

    public GameState currentState;

    public GameObject playerPrefab;

    public GameObject player;


    void Start()
    {
        ui = GetComponent<GameplayUI>();
        GameStart();
    }


    private void GameStart()
    {
        player = Instantiate(playerPrefab);
        StartCoroutine(GameActive(0));

    }

    private IEnumerator GameActive(int index)
    {
        currentState = (GameState)index;

        string title = "";

        switch(currentState)
        {
            case GameState.SKINNING:
                title = "Skinning";
                break;
            case GameState.TANNING:
                title = "Tanning";
                break;
            case GameState.FLESHING:
                title = "Fleshing";
                break;
            case GameState.STITCHING:
                title = "Stitching";
                break;
            case GameState.FORMING:
                title = "Forming";
                break;
            case GameState.MOUNTING:
                title = "Mounting";
                break;
            default:
                title = "";
                break;
        }

        ui.SetUI(title);

        while(true)
        {
            yield return null;
        }

        StartCoroutine(GameActive(index + 1));
    }

}
