using UnityEngine;
using System.Collections;

public class TutorialPlayer : MonoBehaviour
{
    public Rigidbody2D rb2d;
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

    public bool allowMovement;

    public Vector2 startPosition;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        gravityScaleNormal = rb2d.gravityScale;
        gravityScaleSlam = rb2d.gravityScale * 2f;
        startPosition = transform.position;
    }


    void Update()
    {
        if (TutorialController.controller.startTutorial && Input.GetAxis("Horizontal") != 0)
        {
            TutorialController.controller.startTutorial = false;
            TutorialController.controller.waitingForInput = false;
        }
        if (!allowMovement) return;
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
        if (!grounded && Input.GetButtonDown("Boost") && boosts > 0)
        {
            Boost();
            boosts--;
        }
    }

    public bool Jumping()
    {
        return (!grounded && rb2d.velocity.y > 0);
    }
    public bool Falling()
    {
        return (!grounded && rb2d.velocity.y < 0);
    }
    void FixedUpdate()
    {
        Debug.Log("Jumping " + Jumping() + " Falling " + Falling());
        if (!allowMovement) return;
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), Jumping());
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
            anim.SetFloat("vSpeed", rb2d.velocity.y);
        }
        else anim.SetFloat("vSpeed", -1000);

        if (!slam) Move();
    }

    public void Boost()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(jumpForce * 2f * (facingRight ? 1 : -1), jumpForce));
    }
    public void Jump()
    {
        if (!TutorialController.controller.firstJump)
        {
            TutorialController.controller.StartFirstJump();
        }
        else if (!TutorialController.controller.firstDoubleJump && doubleJump)
        {
            TutorialController.controller.StartFirstJump(true);
        }
        Debug.Log("Jumping");
        anim.SetBool("Grounded", false);
        if (doubleJump) rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(0, (doubleJump ? jumpForce / 1.25f : jumpForce)));
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

    public int coins, stars, boosts;
    public void Collect(Collectable.Type collectable)
    {
        switch (collectable)
        {
            case Collectable.Type.Coin:
                if (!TutorialController.controller.firstCoin)
                {
                    TutorialController.controller.firstCoin = true;
                    TutorialController.controller.StartFirstCoin();
                }
                coins++;
                break;
            case Collectable.Type.Star:
                if (!TutorialController.controller.firstStar)
                {
                    TutorialController.controller.firstStar = true;
                    TutorialController.controller.StartFirstStar();
                }
                stars++;
                break;
            case Collectable.Type.Boost:
                if (!TutorialController.controller.firstBoost)
                {
                    TutorialController.controller.firstBoost = true;
                    TutorialController.controller.StartFirstBoost();
                }
                boosts++;
                break;
            default:
                break;
        }

    }
}
