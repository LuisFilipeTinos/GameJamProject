using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public bool pulando;
    public bool puloDuplo;
    public static int numClick;
    private Rigidbody2D rB;
    public float direction;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        if(Variaveis.podeMover == true)
        {
            Move();
        }

        if(Variaveis.podePular == true)
        {
            Jump();
        }
        
        anim.SetFloat("yVelocity", rB.velocity.y);
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        if(Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("Walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(pulando == false)
            {
                rB.AddForce(new Vector3(0f, jump), ForceMode2D.Impulse);
                puloDuplo = true;
                anim.SetBool("Jump", true);

                StaminaSystem.instance.UseStamina(15);
            }
            else
            {
                if(puloDuplo)
                {
                    rB.AddForce(new Vector3(0f, jump), ForceMode2D.Impulse);
                    puloDuplo = false;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            pulando = false;
            anim.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            pulando = true;
        }
    }
}
