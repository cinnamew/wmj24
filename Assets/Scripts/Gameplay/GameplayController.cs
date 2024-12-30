using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Fungus;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField] string nextScene;
    [SerializeField] Flowchart flowchart;

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
        GameActive();

    }

    public void GameActive()
    {
        currentState = (GameState)1;
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
                StartCoroutine(formingController.FormingGameplay(flowchart));
                break;
            case GameState.STITCHING:
                //if (nextScene != "END") SceneManager.LoadScene(nextScene);
                //else DialogueManager.Instance.ChangeToEnding();
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