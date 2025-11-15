using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator m_Animator;

    [SerializeField] private float xInput;

    [Header("Attack")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy;


    [Header("Animation")]
    [SerializeField] private bool IsMoving;
    [SerializeField] private bool IsFlip;


    [Header("Movements")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;

    [Header("Collision")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask WhatIsGround;
    private bool IsGrounded = false;


    private bool canMove = true;
    private bool canJump = true;

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

    public void DamageEnemies()
    {
        var enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);
        Debug.Log($"enemy --->${enemyColliders.Length}");
        foreach (var enemy in enemyColliders)
        {
            var e = enemy.GetComponent<Enemy>();
            e.TakeDamage();
        }
    }

    public void EnableJumpAndMovement(bool enable)
    {
        canJump = enable;
        canMove = enable;
    }

    // 50/s
    private void FixedUpdate()
    {

    }

    void HandleCollision()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, WhatIsGround);

        // IsGrounded = hit.collider != null;
        // Debug.Log($"ground:{IsGrounded}:${hit.collider.ToString()}");
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryToJump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryToAttack();
        }
    }

    void TryToAttack()
    {
        if (IsGrounded)
        {
            m_Animator.SetTrigger("attack");
        }
    }

    void TryToJump()
    {
        if (IsGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }


    private void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
    }

    private void HandleAnimation()
    {
        // var isMoving = rb.linearVelocityX != 0;
        // if (isMoving != IsMoving)
        // {
        //     IsMoving = isMoving;
        //     m_Animator.SetBool("IsMoving", IsMoving);
        //     Debug.Log($"Set Animation :{IsMoving}");
        // }

        if (rb.linearVelocityX > 0 && IsFlip || rb.linearVelocityX < 0 && !IsFlip)
        {
            IsFlip = !IsFlip;
            // IsFlip ? (transform.rotation = ) : ();
            // transform.Rotate(new Vector3(0, 180, 0));
            transform.Rotate(0, 180, 0);
            Debug.Log($"Flip Animation :{IsFlip}");
        }

        m_Animator.SetBool("isGrounded", IsGrounded);
        m_Animator.SetFloat("xVelocity", rb.linearVelocityX);
        m_Animator.SetFloat("yVelocity", rb.linearVelocityY);
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance, 0));

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

    }

    // [ContextMenu("Flip")]
    // private void Flip()
    // {
    //     transform.Rotate(0, 180, 0);
    // }
}
