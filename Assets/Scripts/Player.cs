using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;

    [SerializeField] private float xInput;


    [Header("Animation")]
    [SerializeField] private bool IsMoving;
    [SerializeField] private bool IsFlip;


    [Header("Movements")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;

    [Header("Collision")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool IsGrounded;
    [SerializeField] private LayerMask WhatIsGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        m_Animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleCollision();
        // xInput = Input.GetAxis("Horizontal");
        HandleInput();
        HandleMovement();
        HandleAnimation();
    }

    // 50/s
    private void FixedUpdate()
    {

    }

    void HandleCollision()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, WhatIsGround);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }


    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);
        if (rb.linearVelocityX > 0 && IsFlip || rb.linearVelocityX < 0 && !IsFlip)
        {
            IsFlip = !IsFlip;
            // IsFlip ? (transform.rotation = ) : ();
            // transform.Rotate(new Vector3(0, 180, 0));
            transform.Rotate(0, 180, 0);
            Debug.Log($"Flip Animation :{IsFlip}");
        }
    }

    private void HandleAnimation()
    {
        var isMoving = rb.linearVelocityX != 0;
        if (isMoving != IsMoving)
        {
            IsMoving = isMoving;
            m_Animator.SetBool("IsMoving", IsMoving);
            Debug.Log($"Set Animation :{IsMoving}");
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));
    }

    // [ContextMenu("Flip")]
    // private void Flip()
    // {
    //     transform.Rotate(0, 180, 0);
    // }
}
