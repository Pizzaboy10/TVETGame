using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 10;                //How fast they move
    public float jumpForce = 1000;          //How high they jump
    private float move;                     //Takes inputs and applies them to physics.
    private Rigidbody2D rb;                 //the Rigidbody to apply forces to

    public bool canJump;

    public bool lookingRight;

    public GameObject attackZone;
    public float attackDuration = 0.1f;     //How long is the hurtbox enabled for?
    public float attackCooldown = 0.25f;    //How long it takes between attacks.
    private float attackTime;               //Keeps track of the next safe time to attack.

    public Animator anim;       //The animation controller

    public GameObject attackPivot;
    public Camera cam;

    public int health = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //Rigidbody reference
        anim = GetComponentInChildren<Animator>();  //The child that displays images
        cam = Camera.main;
    }

    void Update()
    {
        cam.transform.position = transform.position + Vector3.back * 10;


        //Get the direction
        var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);

        //Get the angle
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //Account for looking the other direction
        if (!lookingRight)
        {
            angle += 180;
        }

        //Rotate the object
        attackPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);




        move = Input.GetAxis("Horizontal") * speed;     //Assign current speed

        anim.SetFloat("MoveSpeed", Mathf.Abs(move));    //The speed of the animations.

        if (move > 0 && !lookingRight)
        {
            lookingRight = true;
            transform.localScale = Vector3.one;
        }
        else if (move < 0 && lookingRight)
        {
            lookingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > attackTime + attackCooldown)
            {
                attackZone.SetActive(true);                 //Enable the hurtbox
                attackTime = Time.time + attackDuration;    //Figure out when to turn it off
            }
        }
        if (Time.time > attackTime)                         //When the time is up...
        {
            attackZone.SetActive(false);                    //...Disable hurtbox
        }


        if (canJump && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
            canJump = false;
        }
    }

    private void LateUpdate()
    {
        cam.transform.position = transform.position + new Vector3(0, 1, -10);
    }



    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move, rb.velocity.y); //Apply speed to player
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health = health - 1;
            FindObjectOfType<GameManager2>().HealthChange(health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "Health")
        {
            health++;
            FindObjectOfType<GameManager2>().HealthChange(health);
            Destroy(collision.gameObject);
        }

        Vector3 jumpCheck = transform.position + Vector3.down;  //Gets point below player

        if (Physics2D.OverlapPoint(jumpCheck))
        {
            canJump = true;
        }
    }
}
