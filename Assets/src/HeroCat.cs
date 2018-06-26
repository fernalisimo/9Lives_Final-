using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HeroCat : MonoBehaviour
{

    public float speed = 1f;
    private bool alive = true;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D bc;

    public float jumpSpeed = 2f;
    public float maxJumpTime = 2f;
    private float jumpTime = 0;
    private bool grounded;
    private bool jumpActive;

    private Mode currentMode;

    public enum Mode
    {
        Idle, Run, Jump, Fall, Die
    }

// Use this for initialization
	void Start ()
	{
        // save initial cat position
        LevelController.current.SetStartPosition(transform.position);

	    sr = GetComponent<SpriteRenderer>();
	    rb = GetComponent<Rigidbody2D>();
	    animator = GetComponent<Animator>();
	    bc = GetComponent<BoxCollider2D>();

        currentMode = Mode.Idle;
	}
	
	// Update is called once per frame
	void Update () {

	    animator.SetBool("run", false);
	    animator.SetBool("jump", false);
	    animator.SetBool("fall", false);

        if (currentMode == Mode.Idle)
	    {

        }
	    else if (currentMode == Mode.Run)
	    {
            animator.SetBool("run", true);
	    }
	    else if (currentMode == Mode.Jump)
	    {
            animator.SetBool("jump", true);
	    }
	    else if (currentMode == Mode.Fall)
	    {
            animator.SetBool("fall", true);
	    }
    }

    void FixedUpdate()
    {
        if (alive)
        {
            Movement();
            Jump();
        }
    }

    public bool IsAlive()
    {
        return alive;
    }

    // === MOVEMENT ===
    private void Movement()
    {
        float dir = Input.GetAxis("Horizontal");

        if (dir == 0)
        {
            currentMode = Mode.Idle;
        }
        else
        {
            if (dir < 0)
            {
                sr.flipX = true;

            }
            else if (dir > 0)
            {
                sr.flipX = false;
            }

            currentMode = Mode.Run;
        }

        transform.Translate(new Vector3(dir, 0) * speed * Time.deltaTime);
    }

    // === JUMP ===
    private void Jump()
    {
        GroundedCheck();

        // === JUMPING ===
        // jump button was just pressed
        if (Input.GetButton("Jump") && grounded)
        {
            jumpActive = true;
        }

        if (jumpActive)
        {
            // if jump button is still being held
            if (Input.GetButton("Jump"))
            {
                jumpTime += Time.deltaTime;
                if (jumpTime < maxJumpTime)
                {
                    Vector2 vel = rb.velocity;
                    vel.y = jumpSpeed * (1.0f - jumpTime / maxJumpTime);
                    rb.velocity = vel;

                    currentMode = Mode.Jump;
                }
            }
            else
            {
                jumpActive = false;
                jumpTime = 0;   
            }
        }

    }

    // === GROUNDED CHECK ===
    private void GroundedCheck()
    {
        Vector3 from = transform.position + Vector3.down;
        Vector3 to = transform.position + Vector3.down * 1.15f;
        int layerId = 1 << LayerMask.NameToLayer("Ground");

        Vector3 offsetLeft = new Vector3(-bc.size.x * 0.25f, 0);
        Vector3 offsetRight = new Vector3(bc.size.x * 0.25f, 0);

        RaycastHit2D hit = Physics2D.Linecast(from, to, layerId);
        RaycastHit2D hitLeft = Physics2D.Linecast(from + offsetLeft, to + offsetLeft, layerId);
        RaycastHit2D hitRight = Physics2D.Linecast(from + offsetRight, to + offsetRight, layerId);

        //Debug.DrawLine(from, to, Color.red);
        //Debug.DrawLine(from + offsetLeft, to + offsetLeft, Color.red);
        //Debug.DrawLine(from + offsetRight, to + offsetRight, Color.red);

        if (hit || hitLeft || hitRight)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
            currentMode = Mode.Fall;
        }

    }

    // === DEATH ===
    public void Die()
    {
        alive = false;
        animator.SetTrigger("die");
        StartCoroutine(DieLater());
    }

    IEnumerator DieLater()
    {
        yield return new WaitForSeconds(GetDeathAnimationLength() - 0.1f);
        alive = true;
        LevelController.current.OnCatDeath(this);
    }

    // how long does one cycle of death animation lasts
    private float GetDeathAnimationLength()
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;    //Get Animator controller
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "CatDeath")
            {
                return ac.animationClips[i].length;
            }
        }

        return 1;

    }

    public SpriteRenderer GetSR()
    {
        return sr;
    }



}
