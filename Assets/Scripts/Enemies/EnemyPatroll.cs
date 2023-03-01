using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    private bool isFacingLeft;
    Rigidbody2D rb2d;
    SpriteRenderer sr;
    private float moveSpeed = 200f;
    [SerializeField] EnemyTakeDamage damageScript;

    [SerializeField] Transform castPos;
    [SerializeField] float baseCastDist;

    [SerializeField] float shakeSpeed = 5.0f;
    [SerializeField] float shakeAmount = 1.0f;

    [SerializeField] BoxCollider2D collider;
    [SerializeField] BoxCollider2D trigger;

    [SerializeField] ParticleSystem groundParticles;

    [SerializeField] Transform castPoint;
    [SerializeField] float agroRange;
    [SerializeField] Transform playerTransf;

    [SerializeField] bool appeared;

    private void Start()
    {
        isFacingLeft = true;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        collider.enabled = false;
        groundParticles.Pause();
        rb2d.gravityScale = 0;
        appeared = false;
    }

    private void FixedUpdate()
    {
        if (!damageScript.isShaking && appeared)
        {
            if (isHittingWallOrCloseEdge(true) || isHittingWallOrCloseEdge(false))
                ChangeDirection();

            if (!damageScript.isDying)
            {
                //if (CanSeePlayer(agroRange))
                //{
                //    ChasePlayer();
                //}
                if (!isFacingLeft)
                    rb2d.velocity = new Vector2(moveSpeed * Time.deltaTime, 0);
                else
                    rb2d.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0);
            }
        }
        else if (!appeared && !damageScript.isDying)
            this.transform.position = new Vector2(this.transform.position.x + Mathf.Sin(Time.time * 20.0f) * 0.01f, this.transform.position.y);
    }

    //private bool CanSeePlayer(float distance)
    //{
    //    float castDist = distance;

    //    if (isFacingLeft)
    //        castDist = -distance;

    //    Vector2 endPos = castPoint.position + Vector3.right * castDist;

    //    RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("IgnoreBloodContact"));

    //    if (hit.collider != null)
    //    {
    //        Debug.DrawLine(castPoint.position, endPos, Color.yellow);

    //        var magnitude = 4;
    //        if (isFacingLeft)
    //            magnitude = -4;

    //        //transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(playerTransf.position.x, transform.position.y), magnitude * Time.deltaTime);
    //        transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, playerTransf.position.x, magnitude), transform.position.y);

    //        if (hit.collider.gameObject.CompareTag("Player"))
    //            return true;
    //        else
    //            return false;
    //    }
    //    else
    //    {
    //        Debug.DrawLine(castPoint.position, endPos, Color.blue);
    //        return false;
    //    }
            
    //}

    private void ChasePlayer()
    {
        if (transform.position.x > playerTransf.position.x)
        {
            //rb2d.velocity = new Vector2(-moveSpeed, 0);
            this.transform.localScale = new Vector3(-2, 2, 2);
            isFacingLeft = false;
        }
        else
        {
            //rb2d.velocity = new Vector2(moveSpeed, 0);
            this.transform.localScale = new Vector3(2, 2, 2);
            isFacingLeft = true;
        }
    }

    private void ChangeDirection()
    {
        if (isFacingLeft)
        {
            this.transform.localScale = new Vector3(-2, 2, 2);
            isFacingLeft = false;
        }
        else
        {
            this.transform.localScale = new Vector3(2, 2, 2);
            isFacingLeft = true;
        }
    }

    bool isHittingWallOrCloseEdge(bool wall = true)
    {
        bool val;
        float castDist = baseCastDist;

        Vector3 targetPos = castPos.position;

        if (wall)
        {
            val = false;
            targetPos.x += castDist;

            Debug.DrawLine(castPos.position, targetPos, Color.blue);

            if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
                val = true;
        }
        else
        {
            val = true;
            targetPos.y -= castDist;

            Debug.DrawLine(castPos.position, targetPos, Color.red);

            if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
                val = false;
        }


        return val;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAppear") && !appeared)
            StartCoroutine(EnemyAppear());
    }

    private IEnumerator EnemyAppear()
    {
        trigger.enabled = false;
        groundParticles.Play();
        rb2d.velocity = new Vector2(rb2d.velocity.x, 1);
        yield return new WaitForSeconds(1.8f);
        rb2d.velocity = Vector2.zero;
        groundParticles.Stop();
        collider.enabled = true;
        rb2d.gravityScale = 1;
        appeared = true;
        damageScript.finishedAppearing = true;
    }
}
