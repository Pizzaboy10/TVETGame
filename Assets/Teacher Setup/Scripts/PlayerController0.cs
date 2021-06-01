using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController0 : MonoBehaviour
{
    GameManager0 manager;               

    public int health = 3;
    float lastHitTime = 0.0f;
    public float iFrames = 0.5f;

    public float speed = 10;
    public float jumpForce = 100;
    private float move = 0;
    public bool canJump;
    private Rigidbody2D rb;
    private bool lookRight = true;

    public GameObject attackPivot;          //Point that the attack zone goes around
    public GameObject attackZone;
    public float attackDuration = 0.15f;    //How long the attack hurtbox lasts
    public float attackCooldown = 0.25f;    //How much time you need to wait between attacks
    private float attackTime = 0f;          //When the attack hurtbox dissapears

    public Camera cam;

    public Animator anim;           //ANIMATOR REFERENCE

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //Rigidbody reference
        cam = Camera.main;
        manager = FindObjectOfType<GameManager0>();
    }

    void Update()
    {
        MoveAround();
        Attack();
    }
    private void LateUpdate()
    {
        //Avoids camera jitter
        CameraControl();
    }

    void CameraControl()
    {
        Vector3 offset = new Vector3(0, 1, -10);
        cam.transform.position = transform.position + offset;
    }

    void Attack()
    {
        //Get the Mouse position
        var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        
        //Get the angle
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //Accoun for looking the other direction
        if (!lookRight)
            angle += 180;

        //Rotate the object
        attackPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



        if (Input.GetButtonDown("Fire1") && Time.time > (attackTime + attackCooldown)) //If we attack and we are not already attcking
        {
            attackZone.SetActive(true);                             //Enable hurtbox
            attackTime = Time.time + attackDuration;                //Set a duration
        }
        if (Time.time > attackTime)
        {
            attackZone.SetActive(false);                            //Disable hurtbox.
        }
    }

    void MoveAround()
    {
        move = Input.GetAxis("Horizontal") * speed;                 //Determine horizontal speed from inputs.

        anim.SetFloat("MoveSpeed", Mathf.Abs(move));     //Makes reversing not negative


        if (move > 0 && !lookRight)
        {
            lookRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0 && lookRight)
        {
            lookRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (canJump && Input.GetButtonDown("Jump"))                 //Check for jump ability and input
        {
            rb.AddForce(Vector2.up * jumpForce);                    //Apply force.
            canJump = false;
        }
    }


    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(move, rb.velocity.y);             //Move horizontally due to inputs, 
                                                                    //and vertically due to current movements
        rb.velocity = dir;                                          //Apply the velocity
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.OverlapPoint(transform.position + Vector3.down))//Check if there is an object below us.
        {
            canJump = true;                                         //if there was, we can jump
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (Time.time > lastHitTime + iFrames)
            {
                health--;
                manager.HealthChange(health);
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
            lastHitTime = Time.time;
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            health++;
            manager.HealthChange(health);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 8)
        {
            manager.GetCoin();
            Destroy(collision.gameObject);
        }
    }

}