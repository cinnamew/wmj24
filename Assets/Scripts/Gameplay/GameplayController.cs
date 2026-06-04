using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Fungus;
using TMPro;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField] string nextScene;
    [SerializeField] Flowchart flowchart;
    [SerializeField] TMP_Text instructions;

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

        if (SceneManager.GetActiveScene().name != "Minigame3") GameStart();
    }


    public void GameStart()
    {
        player = Instantiate(playerPrefab);
        GameActive();

    }

    public void GameActive(int index = 0)
    {
        //currentState = (GameState)1;
        isComplete = false;

        string title = "";
        if (SceneManager.GetActiveScene().name == "Minigame4") index = 1;
        currentState = (GameState)index;

        switch (currentState)
        {
            case GameState.SKINNING:
                title = "Skinning";
                instructions.text = "Move your mouse to cut out the animal!";
                StartCoroutine(skinningController.SkinningGameplay(flowchart));
                break;
            case GameState.FORMING:
                title = "Forming";
                instructions.text = "Click the screen when prompted. You're felting a beautiful animal!";
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