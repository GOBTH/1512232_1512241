using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kitty : MonoBehaviour
{
    public float jumpForce;
    public float speed = 50f;
    private Animator anim;
    private float translation;
    public LayerMask whatIsGround;
    public Transform groundPosition;
    public bool Grounded = true;
    public GameObject particle;
    private Rigidbody2D rb;
    private bool isDead = false;
    private bool control = true;
    public Text ScoreText;
    public float getXpos 
    { 
        get {
            return transform.position.x;
        }
    }
    public float getYpos
    {
        get
        {
            return transform.position.y;
        }
    }
    public bool movingRight
    {
        get
        {
            if (translation == 1)
                return true;
            return false;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            control = true;
            jumb();
        }
    }

    void FixedUpdate()
    {
        if (isDead)
            return;
        
        //Grounded = isGrounded();
        if(Application.platform == RuntimePlatform.WindowsEditor && control == true)
        {
            //di chuyen
            translation = Input.GetAxisRaw("Horizontal");
        }

        
        TurnPlayer();
        //transform.Translate(new Vector3(translation, 0f, 0f) * Time.deltaTime * speed);
        rb.velocity = new Vector2(translation * Time.deltaTime * speed, rb.velocity.y);

        if (translation > 0 || translation < 0)
        {
            anim.SetFloat("speed", 20);
        }
        if (translation == 0)
        {
            anim.SetFloat("speed",0);
        }
    }
    void TurnPlayer() {
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
    public void jumb()
    {
        if (!isGrounded())
        {
            return;
        }
        rb.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "coin")
        {
            ScoreText.text =(int.Parse(ScoreText.text)+5).ToString();
            GameObject p = Instantiate(particle, col.transform.position,particle.transform.rotation);
            Destroy(p, 0.5f);
            Destroy(col.gameObject);
        }
        if (col.tag == "home")
        {
            if(transform.childCount > 1)
            {
                GameObject chicken = transform.GetChild(1).gameObject;
                chicken.GetComponent<Chicken>().follow = false;
                chicken.transform.parent = null;
                chicken.GetComponent<Collider2D>().enabled = false;
                chicken.GetComponent<ChickenRun>().enabled = true;
                StartCoroutine(ChickenDestroy(chicken));
            }
        }
    }

    IEnumerator ChickenDestroy(GameObject chicken)
    {
        yield return new WaitForSeconds(1);
        chicken.SetActive(false);
        Destroy(chicken);

        int ChickenCount = GameObject.FindGameObjectsWithTag("Chicken").Length;

        if(ChickenCount == 0)
        {
            UIHandler.instance.ShowLevelDialog("Level Cleard", ScoreText.text);
            
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Max")
        {
            if (isDead)
                return;
            anim.SetTrigger("death");
            isDead = true;
            UIHandler.instance.ShowLevelDialog("Level Failed...", ScoreText.text);

        }
    }

    public void OnPointerEnter_Right()
    {
        control = false;
        translation = 1;
    }
    public void OnPointerExit()
    {
        control = false;
        translation = 0;
    }
    public void OnPointerEnter_Left()
    {
        control = false;
        translation = -1;
    }

}
