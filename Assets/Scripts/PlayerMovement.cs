using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    public GameObject rightSwing;
    public GameObject leftSwing;
    MovementState state;
    public float KBForce; //represents force which player knocks back enemy
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

 
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Trampy;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    public float fastFallMultiplier = 2f;

    public enum MovementState { idle, running, jumping, falling, attack}
    

    // Start is called before the first frame update
    private void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
       anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if(KBCounter  <= 0)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);//left or right code
        }
        else
        {
            if(KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if(KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }


            KBCounter -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump") && IsGrounded())//jump code and 1 jump only
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetMouseButton(0) && state != MovementState.attack)
        {

            StartCoroutine(AttackCo());

        }
        UpdateAnimationState();

    }
    private void FixedUpdate()
    {
        
        
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -fastFallMultiplier * Mathf.Abs(rb.velocity.y));
        }
 

    }

    private void UpdateAnimationState()//runing animation code
    {

        if (state != MovementState.attack)
        {
            if (dirX > 0f)
            {
                state = MovementState.running;
                sprite.flipX = false;
            }
            else if (dirX < 0f)
            {
                state = MovementState.running;
                sprite.flipX = true;
            }
            else
            {
                state = MovementState.idle;
            }

            if (rb.velocity.y > .1f)
            {
                state = MovementState.jumping;
            }
        }

        anim.SetInteger("state", (int)state);
    }
  
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, Ground);

    }


    private IEnumerator AttackCo() // attack
    {
        anim.SetBool("attacking", true);
        state = MovementState.attack;
        yield return new WaitForSeconds(.01f);

        // Check the direction the player is facing
        if (sprite.flipX == false) // Facing right
        {
            rightSwing.SetActive(true);
            leftSwing.SetActive(false);
        }
        else // Facing left
        {
            leftSwing.SetActive(true);
            rightSwing.SetActive(false);
        }

        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        leftSwing.SetActive(false);
        rightSwing.SetActive(false);
        state = MovementState.idle;
    }




}

