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
        FORMING,
        STITCHING,
        MOUNTING
    }

    public GameState currentState;

    public GameObject playerPrefab, razorPrefab;

    public GameObject player;
    public bool isComplete;


    void Start()
    {
        ui = GetComponent<GameplayUI>();
        GameStart();
    }


    private void GameStart()
    {
        player = Instantiate(playerPrefab);
        GameActive(0);

    }

    private void GameActive(int index)
    {
        currentState = (GameState)index;
        isComplete = false;

        string title = "";

        switch (currentState)
        {
            case GameState.SKINNING:
                title = "Skinning";
                StartCoroutine(Skinning());
                break;
            case GameState.TANNING:
                title = "Tanning";
                break;
            case GameState.FORMING:
                title = "Forming";
                break;
            case GameState.STITCHING:
                title = "Stitching";
                break;
            case GameState.MOUNTING:
                title = "Mounting";
                break;
            default:
                title = "";
                break;
        }

        ui.SetUI(title);

    }


    //Skinning
    public IEnumerator Skinning()
    {
        GameObject razor = Instantiate(razorPrefab);

        while (!isComplete)
        {
            yield return null;
        }
    }






}