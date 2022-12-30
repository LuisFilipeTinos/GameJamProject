using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    private Vector3 newPosition;

    private void Update()
    {
        // transform.position = Vector2.Lerp(transform.position, player.position, 0.1f);
        newPosition = transform.position;
        newPosition.x = player.position.x;
        newPosition.y = player.position.y + 1.9f;
        transform.position = newPosition;
    }
}
