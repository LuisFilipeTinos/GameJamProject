using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator anim;

    //Variables:
    [SerializeField] float moveSpeed = 240f;
    [SerializeField] float jumpingSpeed = 500f;

    bool movingRight, movingLeft;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckL;

    [SerializeField]
    Transform groundCheckR;

    [SerializeField] bool isGrounded;

    Vector2 vecGravity;
    [SerializeField] float fallMultiplier;

    [SerializeField] bool isAttacking;

    [SerializeField] ParticleSystem dust;
    [SerializeField] ParticleSystem fallDust;

    [SerializeField] PlayerTakeDamage playerDamageScript;

    bool canDash = true;
    bool isDashing;
    float dashingPower = 3f;
    float dashingTime = 0.2f;
    float dashingCooldown = 1f;

    [SerializeField] GameObject bulletPrefab;

    public bool isFacingLeft;

    public bool canMove;
    public bool jumpedOnce = false;

    private float normalizedJumpForce = 17.40f;

    private float colliderOffsetYSlide = -0.1014729f;
    private float colliderSizeYSlide = 0.09066391f;

    private float colliderOffsetY = 0.001597583f;
    private float colliderSizeY = 0.2968048f;

    [SerializeField]
    private BoxCollider2D coll;
    [SerializeField]
    private SpriteRenderer sr;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        isFacingLeft = false;
        canMove = true;
        jumpedOnce = false;
    }

    void Update()
    {
        if (canMove)
        {
            if (isDashing)
            {
                ShadowsSprite.me.ShadowsSkill();
                return;
            }


            if (playerDamageScript.knockBackCounter <= 0)
            {
                if (!isGrounded)
                    anim.Play("JumpAnim");

                if (Input.GetKeyDown(KeyCode.V))
                {
                    GameObject newBullet = Instantiate(bulletPrefab, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    newBullet.GetComponent<PlayerBulletScript>().StartShoot(isFacingLeft);
                }

                if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.C) && canDash)
                    StartCoroutine(Slide());
                else if (Input.GetKeyDown(KeyCode.C) && canDash && !isGrounded)
                    StartCoroutine(Dash());

                if (Input.GetKeyDown(KeyCode.F))
                    playerDamageScript.Deffending();
                if (Input.GetKeyUp(KeyCode.F))
                    playerDamageScript.IsntDeffending();

                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    coll.offset = new Vector2(coll.offset.x, colliderOffsetYSlide);
                    coll.size = new Vector2(coll.size.x, colliderSizeYSlide);
                }
                else
                {
                    coll.offset = new Vector2(coll.offset.x, colliderOffsetY);
                    coll.size = new Vector2(coll.size.x, colliderSizeY);
                }

                if (Input.GetKeyDown(KeyCode.Z) && !isAttacking && isGrounded)
                {
                    rb2d.velocity = Vector2.zero;
                    rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                    anim.Play("AttackAnim");
                    isAttacking = true;
                    StartCoroutine(WaitToAttackAgain());
                }


                if (rb2d.velocity.y < 0)
                    rb2d.velocity -= vecGravity * fallMultiplier * Time.deltaTime;

                if (!isAttacking)
                {
                    if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
                    {
                        isFacingLeft = false;

                        if (isGrounded)
                            anim.Play("WalkAnim");

                        if (!isAttacking)
                        {
                            movingRight = true;
                            movingLeft = false;
                        }
                        else
                        {
                            movingRight = false;
                            movingLeft = false;
                        }

                        this.transform.localScale = new Vector3(5, 5, 5);
                    }
                    else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !isAttacking)
                    {
                        isFacingLeft = true;

                        if (isGrounded)
                            anim.Play("WalkAnim");

                        if (!isAttacking)
                        {
                            movingLeft = true;
                            movingRight = false;
                        }
                        else
                        {
                            movingRight = false;
                            movingLeft = false;
                        }

                        this.transform.localScale = new Vector3(-5, 5, 5);
                    }
                    else if (!isAttacking)
                    {
                        if (isGrounded)
                            anim.Play("IdleAnim");

                        movingLeft = false;
                        movingRight = false;
                    }

                    if (Input.GetKeyDown(KeyCode.X) && jumpedOnce && !isGrounded)
                    {
                        if (jumpedOnce)
                            jumpedOnce = false;

                        CreateDust();
                        anim.Play("JumpAnim");

                        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                        rb2d.velocity = new Vector2(rb2d.velocity.x, normalizedJumpForce);
                    }
                }
            }
        }
        else
        {
            if (isGrounded)
                anim.Play("IdleAnim");
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
            return;

        if (playerDamageScript.knockBackCounter <= 0)
        {
            if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
                isGrounded = true;
            else
                isGrounded = false;

            if (movingRight)
                rb2d.velocity = new Vector2(moveSpeed * Time.deltaTime, rb2d.velocity.y);
            else if (movingLeft)
                rb2d.velocity = new Vector2(-moveSpeed * Time.deltaTime, rb2d.velocity.y);
            else
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            if (Input.GetKey(KeyCode.X) && isGrounded && !isAttacking && canMove)
            {
                CreateDust();
                anim.Play("JumpAnim");
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpingSpeed * Time.deltaTime);
                Invoke("SetJumpedOnceValue", 0.09f);
            }
        }
        else
        {
            if (playerDamageScript.knockFromRight)
                rb2d.velocity = new Vector2(-playerDamageScript.knockBackForce * 1.6f, playerDamageScript.knockBackForce);
            else
                rb2d.velocity = new Vector2(playerDamageScript.knockBackForce * 1.6f, playerDamageScript.knockBackForce);

            playerDamageScript.knockBackCounter -= Time.deltaTime;
        }
    }

    private void SetJumpedOnceValue()
    {
        if (!jumpedOnce)
            jumpedOnce = true;
    }

    private IEnumerator WaitToAttackAgain()
    {
        yield return new WaitForSeconds(0.42f);
        isAttacking = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    public void CreateDust()
    {
        dust.Play();
    }

    public void CreateFallDust()
    {
        fallDust.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            CreateFallDust();
            jumpedOnce = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator Slide()
    {
        coll.offset = new Vector2(coll.offset.x, colliderOffsetYSlide);
        coll.size = new Vector2(coll.size.x, colliderSizeYSlide);
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        coll.offset = new Vector2(coll.offset.x, colliderOffsetY);
        coll.size = new Vector2(coll.size.x, colliderSizeY);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator WaitToDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }

}
