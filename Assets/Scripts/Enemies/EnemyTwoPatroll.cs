using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoPatroll : MonoBehaviour
{
    private bool isFacingLeft;
    Rigidbody2D rb2d;
    private float moveSpeed = 200f;
    [SerializeField] EnemyTwoTakeDamage damageScript;

    [SerializeField] Transform castPos;
    [SerializeField] float baseCastDist;

    [SerializeField] Transform castPoint;
    [SerializeField] float agroRange;
    [SerializeField] Transform playerTransf;

    private void Start()
    {
        isFacingLeft = true;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isHittingWallOrCloseEdge(true) || isHittingWallOrCloseEdge(false))
            ChangeDirection();

        if (!damageScript.isDying)
        {
            //float castDist = agroRange;

            //if (isFacingLeft)
            //    castDist = -agroRange;

            //Vector2 endPos = castPoint.position + Vector3.right * castDist;
            //RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("IgnoreBloodContact"));

            //if (hit.collider != null)
            //{
            //    Debug.DrawLine(castPoint.position, endPos, Color.yellow);

            //    var magnitude = 4f;

            //    transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, playerTransf.position.x, (isFacingLeft == true ? -magnitude : magnitude) * Time.deltaTime), transform.position.y);

            //    if (hit.collider.gameObject.CompareTag("Player"))
            //        ChasePlayer();
            //}
            if (!isFacingLeft)
                rb2d.velocity = new Vector2(moveSpeed * Time.deltaTime, 0);
            else 
                rb2d.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0);
        }
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

    //        var magnitude = 4f;
    //        if (isFacingLeft)
    //            magnitude = -magnitude;

    //        transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, playerTransf.position.x, magnitude * Time.deltaTime), transform.position.y);

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
            this.transform.localScale = new Vector3(-2, 2, 2);
            isFacingLeft = false;
        }
        else
        {
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
}

