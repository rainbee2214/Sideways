using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float maxSpeed = 10f;
    public bool facingRight = true;
    Animator anim;
    float vSpeed;

    public bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 200;

    public bool doubleJump;

    public bool slam = false;

    float gravityScaleSlam;
    float gravityScaleNormal;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        gravityScaleNormal = rb2d.gravityScale;
        gravityScaleSlam = rb2d.gravityScale * 2f;
    }

    void Update()
    {
        if ((grounded || !doubleJump) && Input.GetButtonDown("Jump"))
        {
            if (grounded) Jump();
            else if (!grounded && !doubleJump)
            {
                doubleJump = true;
                Jump();
            }

        }

        if (!grounded && Input.GetButtonDown("Slam"))
        {
            slam = true;
            rb2d.gravityScale = gravityScaleSlam;
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), !grounded);
        if (grounded)
        {
            if (slam)
            {
                slam = false;
                rb2d.gravityScale = gravityScaleNormal;
            }
            doubleJump = false;
        }
        anim.SetBool("Grounded", grounded);

        if (grounded) anim.SetFloat("vSpeed", 0f);
        else if (!slam)
        {
            Debug.Log(vSpeed);
            anim.SetFloat("vSpeed", rb2d.velocity.y);
        }
        else anim.SetFloat("vSpeed", -1000);

        if (!slam) Move();
    }

    public void Jump()
    {
        Debug.Log("Jumping");
        anim.SetBool("Grounded", false);
        rb2d.AddForce(new Vector2(0, jumpForce));
    }

    public void Move()
    {
        //Moving
        float move = Input.GetAxis("Horizontal");
        if (!grounded) move *= 0.5f;
        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);

        if ((move > 0 && !facingRight)) Flip();
        else if (move < 0 && facingRight) Flip();
        anim.SetFloat("Speed", Mathf.Abs(move));
    }



    void Flip()
    {
        facingRight = !facingRight;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }
}
