using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Max : MonoBehaviour
{
    public float jumpForce;
    public float speed = 50f;
    private Animator anim;
    private float translation;
    public LayerMask whatIsGround;
    public Transform groundPosition;
    public bool Grounded = true;

    private Kitty kitty;
    private bool allowJump = false;
    private Rigidbody2D rb;

    void Start()
    {
        kitty = GameObject.FindGameObjectWithTag("Player").GetComponent<Kitty>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        translation = 1;
    }
    void Update()
    {

    }

    void FixedUpdate()
    {
        TurnPlayer();
        //Grounded = isGrounded();
        //di chuyen
        //translation = Input.GetAxisRaw("Horizontal");

        AI();
        

        //transform.Translate(new Vector3(translation, 0f, 0f) * Time.deltaTime * speed);
        rb.velocity = new Vector2(translation * Time.deltaTime * speed, rb.velocity.y);

        if (translation > 0 || translation < 0)
        {
            anim.SetFloat("speed", 1);
        }
        if (translation == 0)
        {
            anim.SetFloat("speed", 0);
        }
    }
    void TurnPlayer()
    {
        if (translation < 0)
        {
            //doi huong di chuyen
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (translation > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    bool isGrounded()
    {
        if (Physics2D.OverlapCircle(groundPosition.position, 0.3f, whatIsGround))
        {
            return true;
        }
        return false;
    }
    void jumb()
    {
        if (!isGrounded())
        {
            return;
        }

        /*Vector3 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;*/

        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "jump")
        {
            allowJump = true;
            LeftRightDecision();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "jump")
        {
            allowJump = false;
        }
    }
    public void SwitchDirection()
    {
        if (translation == 1)
            translation = -1;
        else translation = 1;
    }
    void LeftRightDecision()
    {
        //khi kitty ben trai Max
        if (kitty.getXpos - 0.5f > transform.position.x)
        {
            translation = 1f;
        }
        else if (kitty.getXpos + 0.5f < transform.position.x)
        {
            //khi kitty ben phai Max
            translation = -1f;
        }
    }
    void AI()
    {

        
        

        if (kitty.getXpos - 0.5f > transform.position.y && allowJump)
        {
            //quyết định nhảy, khi kitty đang ở vị trí cao hơn
            jumb();
        }


    }
}
