using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region PlayGameObjects
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private TrailRenderer playerTR;
    [SerializeField] private ParticleSystem playerJumpPS;
    [SerializeField] private AudioSource dashSound;
    public static AudioSource staticDestroySound;
    public AudioSource destroySound;

    #endregion

    #region Moving and Jumping
    [Header("Moving and jumping")]
    private float movingHorizontal;
    private readonly float moveSpeed = 250f;
    private readonly float jumpForce = 10f;
    private bool isJumping;
    #endregion

    #region Dashing
    [Header("Dashing")]
    private readonly float dashingPower = 20f;
    private readonly float dashinTime = 0.2f;
    private readonly float dashinCD = 0.2f;
    private bool canDash = true;
    public static bool isDashing;
    #endregion

    #region Animation
    [Header("Animation")]
    [SerializeField] private Animator animator;
    #endregion

    private void Start()
    {
        staticDestroySound = destroySound;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        movingHorizontal = Input.GetAxisRaw("Horizontal");

        Move();
        Jump();
        Dash();
        Animations();
    }

    private void Move()
    {
        if (movingHorizontal > 0f)
        {
            playerRB.velocity = new Vector2(movingHorizontal * moveSpeed * Time.deltaTime, playerRB.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (movingHorizontal < 0f)
        {
            playerRB.velocity = new Vector2(movingHorizontal * moveSpeed * Time.deltaTime, playerRB.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.W) && !isJumping)
        {
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerJumpPS.Play();
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }

    private void Dash()
    {
        if (Input.GetButton("Dash") && canDash)
        {
            StartCoroutine(Dashing());
        }
    }

    private IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = playerRB.gravityScale;

        playerRB.gravityScale = 0f;
        playerRB.velocity = new Vector2(movingHorizontal * dashingPower, 0f);
        playerTR.emitting = true;

        if (!dashSound.isPlaying)
        {
            dashSound.Play();
        }

        yield return new WaitForSeconds(dashinTime);

        playerTR.emitting = false;
        playerRB.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashinCD);

        canDash = true;
    }

    private void Animations()
    {
        animator.SetFloat("speed", Mathf.Abs(movingHorizontal));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }
}
