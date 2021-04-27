using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;                     //Rigidbody for physics.

    public float speed = 10;                    //Speed multiplier
    private float move = 0;                     //Current movement input.

    public float jumpForce = 300;               //Jumping Force.
    public bool canJump = true;

    public GameObject attackZone;
    public float attackDuration = 0.1f;         //how long the attack lasts
    public float attackCooldown = 0.25f;        //Time between attacks
    private float attackTime;                   //When we can make the next attack


    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal") * speed;
        anim.SetFloat("Movement", Mathf.Abs(move));     //Makes reversing not negative
        if(move > 0)
        {
            transform.localScale = Vector3.one;
        }
        if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            canJump = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(Time.time > attackTime + attackCooldown)
            {
                attackZone.SetActive(true);
                attackTime = Time.time + attackDuration;
            }
        }
        if(Time.time > attackTime)
        {
            attackZone.SetActive(false);
        }


    }

    private void FixedUpdate()
    {
        //Move in the direction of input, and whatever vertical speed they already had
        rb.velocity = new Vector2(move, rb.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 jumpCheck = transform.position + Vector3.down;  
        //Gets point below player

        if (Physics2D.OverlapPoint(jumpCheck))      //Check point
        {
            canJump = true;             //If something there, can jump
        }
    }

}
