using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    /// <summary>
    /// TODO: ADD HEALTH AND TAKE DAMAGE ON CONTACT WITH ENEMY
    /// </summary>
    public int health = 3;
    float lastTimeHit = 0.0f;

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

    public GameObject pivotPoint;
    public Camera cam;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        //get the mouse position.
        var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);

        //Get the angle from the position
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //rotate the pivot
        pivotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



        move = Input.GetAxis("Horizontal") * speed;
        anim.SetFloat("Movement", Mathf.Abs(move));     //Makes reversing not negative
        if(move > 0)
        {
            transform.localScale = Vector3.one;
            pivotPoint.transform.localScale = Vector3.one;
        }
        if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            pivotPoint.transform.localScale = new Vector3(-1, 1, 1);
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

        Vector3 offset = new Vector3(0, 1, -10);
        cam.transform.position = transform.position + offset;
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
        if (collision.gameObject.tag == "Enemy")
        {
            if(Time.time < lastTimeHit + 1.0f) 
            {
                return;
            }
            lastTimeHit = Time.time;

            health--;

            FindObjectOfType<GameManager1>().HealthChange(health);
            
            if (health <= 0)
            {
                rb.freezeRotation = false;
                rb.AddTorque(10);
                anim.enabled = false;
                enabled = false;
            }
        }


        else if (collision.gameObject.tag == "Health")
        {
            health++;
            FindObjectOfType<GameManager1>().HealthChange(health);

            Destroy(collision.gameObject);

        }
    }

}
