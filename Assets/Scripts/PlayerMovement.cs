using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioClip jumpSound;
    private AudioSource audioSource;

    public BoxCollider2D ground;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    private float gravity;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    private float moveX, moveY;
    private bool isFacingRight = true;
    [SerializeField] float climbSpeed = 5f;
    bool climb = false;

    [Header("Jump")]
    private bool jumping = false;
    public float jumpForce = 13.5f;
    private int jumpBufferCounter = 0;
    public int jumpBufferFrames;
    private float coyoteTimeCount = 0;
    public float coyoteTime;
    private float airjumpCount = 0;
    public float maxAirJump;
    private bool isGround;

    [Header("Shot")]
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    private bool isShotting;
    private bool canShot = true;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing = false;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dahingCooldown = 1f;

    public HealthManager healthManager;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    public bool isAlive = true;
    private Vector2 respawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravity = rb.gravityScale;
        audioSource = GetComponent<AudioSource>();
        // Đặt vị trí hồi sinh ban đầu là vị trí bắt đầu game
        respawnPoint = transform.position;
    }

    void Update()
    {
        if (isDashing) return;

        ClimbLadder();
        Die();
        Flip();
        Move();
        Jump();
        OnShot();
        StartDash();
        CheckVerticalState();
        UpdateJumpVariales();

        if ((rb.IsTouchingLayers(LayerMask.GetMask("Climbing")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) && (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || rb.IsTouchingLayers(LayerMask.GetMask("Ground"))) && moveY != 0)
        {
            ground.isTrigger = true;
            Debug.Log("true");
        }
        else
        {
            ground.isTrigger = false;
        }
    }
    public void Move()
    {
        if (!isAlive) { return; }
        if (isDashing || knockbak) return;

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = movement;

        anim.SetBool("isRunning", moveX != 0);
    }

    void Flip()
    {
        if ((moveX > 0 && !isFacingRight) || (moveX < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;


            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    public void StartDash()
    {
        if (Input.GetKeyDown(KeyCode.V) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public IEnumerator Dash()
    {
        anim.SetBool("isDashing", true);
        anim.SetTrigger("Roll");
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dahingCooldown);
        canDash = true;
    }

    public void Done()
    {
        anim.ResetTrigger("Roll");
        anim.SetBool("isDashing", false);
    }
    
    public void Jump()
    {
        if (isDashing || knockbak) return;
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            jumping = false;
        }

        if (!jumping)
        {
            if (jumpBufferCounter > 0 && coyoteTimeCount > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumping = true;
                isGround = false;
                jumpBufferCounter = 0;

                // Phát âm thanh khi nhảy
                audioSource.PlayOneShot(jumpSound);
            }
            else if (!isGround && airjumpCount < maxAirJump && Input.GetKeyDown(KeyCode.Space))
            {
                airjumpCount++;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumping = true;

                // Phát âm thanh khi nhảy trên không
                audioSource.PlayOneShot(jumpSound);
            }

        }
    }

    public void UpdateJumpVariales()
    {
        if (!isAlive) { return; }
        if (isGround)
        {
            jumping = false;
            coyoteTimeCount = coyoteTime;
            airjumpCount = 0;
        }
        else
        {
            coyoteTimeCount -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferFrames;
        }
        else
        {
            jumpBufferCounter = Mathf.Max(jumpBufferCounter - 1, 0);
        }
    }

    public void CheckVerticalState()
    {
        if(climb) return;
        float velocityY = rb.linearVelocity.y;
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGround = true;
        }
            if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (velocityY > 0.1f)
            {
                anim.SetBool("Jump", true);
                anim.SetBool("Fall", false);
            }
            else if (velocityY < -0.1f )
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", true);
            }
        }
        else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
        }
    }

    void OnShot()
    {
        if (!isAlive) { return; }
        if (Input.GetKeyDown(KeyCode.C) && canShot==true)
        {
            anim.SetTrigger("Shot");
            anim.SetBool("isShotting", true);
            StartCoroutine(Shotting());
        }
    }
    public void OnShotComplete()
    {
        anim.ResetTrigger("Shot");
        anim.SetBool("isShotting", false);
    }
    public IEnumerator Shotting()
    {
        canShot = false;
        GameObject shot = Instantiate(bullet, gun.transform.position, Quaternion.identity);
        Rigidbody2D Bullet = shot.GetComponent<Rigidbody2D>();

        if (isFacingRight)
        {
            Bullet.linearVelocity = new Vector2(bulletSpeed, 0);
        }
        else
        {
            Bullet.linearVelocity = new Vector2(-bulletSpeed, 0);
            Vector3 Scale = shot.transform.localScale;
            Scale.x *= -1;
            shot.transform.localScale = Scale;
        }
        yield return new WaitForSeconds(2);
        canShot = true;
    }

    void ClimbLadder()
    {
        if (rb.IsTouchingLayers(LayerMask.GetMask("Climbing")) && moveY != 0)
        {

            anim.SetBool("isClimbing", true);
            climb = true;
        }
        else if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb.gravityScale = gravity;
            anim.SetBool("isClimbing", false);
            climb = false;
            return;
        }

        Vector2 climbVelocity = new Vector2(rb.linearVelocity.x, moveY * climbSpeed);
        rb.linearVelocity = climbVelocity;
        rb.gravityScale = 0f;
        climb = true;

    }

    void ResetAllFallPlatforms()
    {
        GameObject[] fallPlatforms = GameObject.FindGameObjectsWithTag("FallGround");

        foreach (GameObject fallPlatform in fallPlatforms)
        {
            FallPlatform fpScript = fallPlatform.GetComponent<FallPlatform>();
            if (fpScript != null)
            {
                fpScript.ResetFallPlatform();
            }
        }
    }

    [SerializeField] AudioClip pickupSound;
    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            if (!isAlive) return;

            isAlive = false;
            anim.SetTrigger("Dying");

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = pickupSound;
            audioSource.Play();

            Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Enemies", "Hazards"));
            if (collider != null)
            {
                ApplyKnockback(collider.transform.position);
            }
            HealthManager healthManager = FindFirstObjectByType<HealthManager>();
            if (healthManager != null)
            {
                healthManager.LoseLife();
            }
            
            float dieAnimLength = anim.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(Respawn(dieAnimLength));
        }
    }
    public bool reswapm = false;
    public IEnumerator Respawn(float duration)
    {
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        yield return new WaitForSeconds(duration);

        transform.position = respawnPoint;
        rb.simulated = true;
        isAlive = true;

        ResetAnimation();
        FallPlatformManager.Instance.ResetAllPlatforms(); // Reset nền rơi khi hồi sinh
    }




    public void UpDateCheckpoint(Vector2 pos)
    {
        respawnPoint = pos;
    }
    

    void ResetAnimation()
    {
        anim.ResetTrigger("Dying");
        anim.SetBool("isRunning", false);
        anim.SetBool("isClimbing", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Fall", false);
        anim.SetBool("isDashing", false);
        anim.SetBool("isShotting", false);
        anim.Play("Idling"); 
    }

    private bool knockbak = false;
    public void ApplyKnockback(Vector3 sourcePosition)
    {
        knockbak = true;

        // Tính hướng knockback
        Vector2 knockbackDirection = (transform.position - sourcePosition).normalized;

        // Thêm lực knockback
        rb.linearVelocity = new Vector2(knockbackDirection.x * 10f, 8f); // Giảm lực xuống để không bị văng quá mạnh

        // Bắt đầu khôi phục trạng thái
        StartCoroutine(KnockbackRecovery(0.5f));
    }
    private IEnumerator KnockbackRecovery(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Reset trạng thái
        knockbak = false;
        rb.linearVelocity = Vector2.zero; // Đặt lại vận tốc về 0
    }



}
