using System;
using DG.Tweening;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Razor : MonoBehaviour
{
    float speed = 7.0f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, -3), new Vector3(GameplayController.instance.player.transform.position.x, GameplayController.instance.player.transform.position.y, -3), speed * Time.deltaTime);
    }


}
