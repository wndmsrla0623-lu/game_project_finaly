using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 3f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;
    private float moveInput;

    private bool isGiant = false;
    private bool isInvincible = false;
    private bool isSpeedUp = false;
    private bool isJumpUp = false;

    float score;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
        score = 0f;
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (isGiant)
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(2, 2, 2);
            else if (moveInput > 0)
                transform.localScale = new Vector3(-2, 2, 2);
        }
        else
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveInput > 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get <Vector2>();
        moveInput = input.x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("Jump");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Finish"))
        {
            // HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);
            StageResultSaver.SaveStage(SceneManager.GetActiveScene().buildIndex, (int)score);

            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Trap")) 
        {

            if (isGiant || isInvincible)
            {
                if (collision.CompareTag("Enemy")) Destroy(collision.gameObject);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
           
        }

        if (collision.CompareTag("Item"))
        {
            score += 10f;
        }

        if (collision.CompareTag("Item"))
        {
            isGiant = true;
            Invoke(nameof(ResetGiant), 3f);
            Destroy(collision.gameObject);
            score += 10f;
        }

        if (collision.CompareTag("InvincibleItem"))
        {
            isInvincible = true;
            Invoke(nameof(ResetInvincible), 5f);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("SpeedItem"))
        {
            moveSpeed = 4;
            Invoke(nameof(ResetSpeed), 3f);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("JumpItem"))
        {
            jumpForce = 4f;
            Invoke(nameof(ResetJump), 3f);
            Destroy(collision.gameObject);
        }
    }

    void ResetGiant()
    {

    }
    
    void ResetInvincible()
    {
        isInvincible = false;
    }

    void ResetSpeed()
    {
        moveSpeed = 2.3f;
    }

    void ResetJump()
    {
        jumpForce = 3f;
    }
}
