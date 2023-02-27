using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField]
    float speed;

    Rigidbody2D rb2d;

    public void StartShoot(bool isFacingLeft)
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (isFacingLeft)
            rb2d.velocity = new Vector3(-speed, 0, 0);
        else
            rb2d.velocity = new Vector3(speed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
