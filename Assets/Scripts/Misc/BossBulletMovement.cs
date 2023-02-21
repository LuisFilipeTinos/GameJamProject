using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMovement : MonoBehaviour
{
    private float speed = 3;
    private Vector2 target;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
    }
}
