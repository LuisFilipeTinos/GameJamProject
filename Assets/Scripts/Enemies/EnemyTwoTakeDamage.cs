using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoTakeDamage : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float shakeSpeed = 10.0f;
    [SerializeField] float shakeAmount = 0.1f;
    [SerializeField] public bool isShaking;

    [SerializeField] GameObject bloodBurst;
    [SerializeField] float enemyLife;
    Rigidbody2D rb2d;

    [SerializeField] Animator enemyAnim;
    public bool isDying;


    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        isDying = false;
        //isShaking = true;
    }

    public void Update()
    {
        //if (isShaking)
        //   this.transform.position = new Vector2(this.transform.position.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount * 0.2f, this.transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Whip"))
        {
            Instantiate(bloodBurst, this.transform.position, Quaternion.identity);
            if (enemyLife > 0)
            {
                enemyLife--;
                rb2d.velocity = Vector2.zero;
                //isShaking = true;
                StartCoroutine(TakeDamage());
            }
            else
                StartCoroutine(EnemyTwoDeath());
        }
    }

    private IEnumerator TakeDamage()
    {
        //var currentPosition = this.transform.position;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = Color.white;
        //this.transform.position = currentPosition;
        //isShaking = false;
    }

    private IEnumerator EnemyTwoDeath()
    {
        isDying = true;
        rb2d.velocity = Vector2.zero;
        rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        enemyAnim.Play("EnemyTwoDeath");
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
