using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float speed = 2f;
    public float crouchHeight = 0.1f;
    public float jumpHeight = 1f;

    WaitForSeconds oneSecond = new WaitForSeconds(1f);
    Vector2 input;
    Animator anim;
    Rigidbody2D rb2d;
    SpriteRenderer sr;

    public Transform child;

    bool canMove = true, canJump = true, crouching;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetFloat("X", 1);
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        child.transform.position = (Vector2)child.transform.position + Vector2.down*crouchHeight;
    }


    void Update()
    {

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        force = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized*10;
    }

    void FixedUpdate()
    {
        if (input.x != 0)
        {
            if (canMove) Move();

        }
        else
        {
            anim.SetBool("Idle", true);

        }
        if (input.y > 0)
        {
            if (canMove && canJump) Jump();
        }
        else if (input.y < 0)
        {
            if (canMove && !crouching) Crouch();
        }
        else if (input.y == 0)
        {
            if (crouching)
            {
                anim.SetBool("Idle", true);
                anim.SetBool("Crouching", false);
                crouching = false;
            }
        }
    }

    public void Move()
    {
        if (!canMove) return;

        anim.SetBool("Crouching", false);
        anim.SetBool("Idle", false);
        anim.SetFloat("X", input.x);
        rb2d.MovePosition(rb2d.position + input * Time.deltaTime * speed);
    }
    public void Crouch()
    {
        crouching = true;
        anim.SetBool("Idle", false);
        anim.SetBool("Crouching", true);
    }

    public void Jump()
    {
        anim.SetBool("Crouching", false);
        StartCoroutine(StartJump());
    }

    public Vector2 force = new Vector2(100f, 100f);
    IEnumerator StartJump()
    {
        canMove = false;
        canJump = false;
        anim.SetTrigger("Jump");
        rb2d.AddForce(force, ForceMode2D.Impulse);
        yield return oneSecond;

        canJump = true;
        canMove = true;
        yield return null;
    }

}
