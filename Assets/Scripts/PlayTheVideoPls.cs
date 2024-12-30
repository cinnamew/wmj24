using UnityEngine;
using UnityEngine.Video;

public class PlayTheVideoPls : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "deathend.mp4");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
