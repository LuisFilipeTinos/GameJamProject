using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    [SerializeField] float shakeSpeed = 5.0f;
    [SerializeField] float shakeAmount = 1.0f;
    [SerializeField] public bool isShaking;
    [SerializeField] public bool finishedAppearing;

    [SerializeField] GameObject bloodBurst;
    [SerializeField] float enemyLife;
    Rigidbody2D rb2d;

    [SerializeField] BossHealth bossHealth;
    private int deathLoop = 0;

    [SerializeField] Animator enemyAnim;
    public bool isDying;
    

    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        finishedAppearing = false;
        isDying = false;
    }

    public void Update()
    {
        if (isShaking)
            this.transform.position = new Vector2(this.transform.position.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount * 0.2f, this.transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Whip") && finishedAppearing)
        {
            if (this.gameObject.layer == 10)
                bossHealth.health--;

            Instantiate(bloodBurst, this.transform.position, Quaternion.identity);
            if (enemyLife > 0)
            {
                enemyLife--;
                rb2d.velocity = Vector2.zero;
                isShaking = true;
                StartCoroutine(TakeDamage());
            }
            else
            {
                //if (this.gameObject.layer == 10)
                //{
                //    isShaking = true;
                //    //bossAnim.SetTrigger("Death");
                //    StartCoroutine(BossDeath());
                //}
                //else

                //var enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
                //foreach (var enemy in enemiesArray)
                //    Destroy(enemy.gameObject);

                if (this.gameObject.name.Contains("EnemyOne"))
                    StartCoroutine(EnemyOneDeath());
            }

            if (this.gameObject.layer == 10 && bossHealth.health == 3 && this.gameObject.name == "SubBoss")
                enemyAnim.SetBool("Enraged", true);
        }
    }

    private IEnumerator TakeDamage()
    {
        var currentPosition = this.transform.position;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = Color.white;
        this.transform.position = currentPosition;
        isShaking = false;
    }

    private IEnumerator BossDeath()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.07f);
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.07f);
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.07f);
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.07f);
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
        spriteRenderer.color = Color.red;
        deathLoop++;

        if (deathLoop < 12)
            StartCoroutine(BossDeath());
        else
            Destroy(this.gameObject);
    }

    private IEnumerator EnemyOneDeath()
    {
        isDying = true;
        rb2d.velocity = Vector2.zero;
        rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        enemyAnim.Play("EnemyOneDeath");
        yield return new WaitForSeconds(1.1f);
        Destroy(this.gameObject);
    }
}
