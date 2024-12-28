using UnityEngine;

public class Razor : MonoBehaviour
{
    float speed = 5.0f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, GameplayController.instance.player.transform.position, speed * Time.deltaTime);
    }


}
