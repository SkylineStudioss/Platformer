using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;
    public bool isGroundPounding = false;
    public float bounceAmount = 15f;

    [Header("Components")]
    public Rigidbody2D rb;
    //public Animator animator;
    public LayerMask groundLayer;
    public GameObject characterHolder;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    public float groundPoundFallSpeed = 10f;
    public float maxBounce = 30f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;
    public bool collideWithEnemy;
    [HideInInspector]
    public bool justHitEnemy = false;
    [Header("Other")]
    public int enemyCombo;
    private bool canAttack;
    public GameObject hitCollider;
    public GameObject bottomHitCollider;
    private float defaultBounce;
    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    public float checkRadius;
    bool walljumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    public float momentum;
    public LayerMask wallLayer;
    public BoxCollider2D slideColl;
    public BoxCollider2D normColl;
    private bool isSliding = false;
    public float slideSpeed = 10f;
    private bool isCrouching = true;

    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(.75f);
        canAttack = false;
        bottomHitCollider.SetActive(false);
        hitCollider.SetActive(false);
    }

    private void Start()
    {
        defaultBounce = bounceAmount;
    }

    void Slide()
    {
        isSliding = true;
        normColl.enabled = false;
        slideColl.enabled = true;

        if (facingRight && direction.x != 0)
        {
            rb.AddForce(Vector2.right * slideSpeed);
        } else
        {
            rb.AddForce(Vector2.left * slideSpeed);
        }
        if (direction.x == 0)
        {
            Crouch();
        } else
        {
            isCrouching = false;
        }

        StartCoroutine(StopSlide());
    }

    void Crouch()
    {
        isCrouching = true;
    }

    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(0.8f);
        //play slide anim
        normColl.enabled = true;
        slideColl.enabled = false;
        isSliding = false;
    }

    void Update()
    {
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Slide();
        }

        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            canAttack = true;
            hitCollider.SetActive(true);
            bottomHitCollider.SetActive(true);
            StartCoroutine(WaitAttack());
        }

        if (bounceAmount > maxBounce)
        {
            bounceAmount = maxBounce;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, wallLayer);

        if (isTouchingFront == true && onGround == false && direction.x != 0)
        {
            wallSliding = true;
        } else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetButtonDown("Jump") && wallSliding == true)
        {
            walljumping = true;
            Invoke("SetWallJumpingFalse", wallJumpTime);
        }

        if (walljumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -direction.x, yWallForce);
        }

        if (Input.GetKeyDown(KeyCode.S) && onGround == false || Input.GetKeyDown(KeyCode.DownArrow) && onGround == false && justHitEnemy == false)
        {
            GroundPound();
        } else if (onGround)
        {
            gravity = 1f;
            isGroundPounding = false;
        }

        //animator.SetBool("onGround", onGround);
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    void FixedUpdate()
    {
        MoveCharacter(direction.x);
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }

        if (onGround && justHitEnemy == false)
        {
            enemyCombo = 0;
            bounceAmount = defaultBounce;
        }

        
        modifyPhysics();
    }

    void GroundPound()
    {
        gravity = groundPoundFallSpeed;
        isGroundPounding = true;
    }

    void MoveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        //animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        //animator.SetFloat("vertical", rb.velocity.y);
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    public IEnumerator WaitBeforePound()
    {
        yield return new WaitForSeconds(0.25f);
        justHitEnemy = false;
    }

    void SetWallJumpingFalse()
    {
        walljumping = false;
    }
}
