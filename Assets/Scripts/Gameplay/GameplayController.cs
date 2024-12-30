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
    public Skinning skinningController;
    public Forming formingController;
    public Stitching stitchingController;

    public enum GameState
    {
        SKINNING,
        FORMING,
        STITCHING
    }

    public GameState currentState;

    public GameObject playerPrefab, razorPrefab;

    public GameObject player;
    public bool isComplete;


    void Start()
    {
        ui = GetComponent<GameplayUI>();

        skinningController = GetComponent<Skinning>();
        formingController = GetComponent<Forming>();
        stitchingController = GetComponent<Stitching>();

        GameStart();
    }


    private void GameStart()
    {
        player = Instantiate(playerPrefab);
        GameActive(0);

    }

    private void GameActive(int index)
    {
        // currentState = (GameState)index;
        isComplete = false;

        string title = "";

        switch (currentState)
        {
            case GameState.SKINNING:
                title = "Skinning";
                StartCoroutine(skinningController.SkinningGameplay());
                break;
            case GameState.FORMING:
                title = "Forming";
                StartCoroutine(formingController.FormingGameplay());
                break;
            case GameState.STITCHING:
                title = "Stitching";
                StartCoroutine(stitchingController.StitchingGameplay());
                break;
            default:
                title = "";
                break;
        }

        ui.SetUI(title);

    }




}