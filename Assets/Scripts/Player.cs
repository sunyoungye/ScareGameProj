using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float acceleration = 10f;
    public float jumpForce = 15f;
    public int maxJumps = 2;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D coll;
    private int jumpsRemaining;
    private bool isGrounded;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = isRunning ? runSpeed : walkSpeed;

        if (moveInput != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, acceleration * Time.deltaTime);
        }

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }

    void HandleJump()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, coll.bounds.extents.y + 0.1f, groundLayer);
        if (isGrounded)
        {
            jumpsRemaining = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;
        }
    }
}
