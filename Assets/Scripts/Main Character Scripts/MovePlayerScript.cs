using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 1f;
    [SerializeField]
    private float groundCheckDistance = 0.02f;
    [SerializeField]
    private float offsetY = 0.17f;
    [SerializeField]
    private string groundTag = "Ground";
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb2D;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        switch(GameManager.Instance.playerSelect)
        {
            case GameManager.Player.Fox:
                animator.runtimeAnimatorController = GameManager.Instance.collectAnimator[0];
                spriteRenderer.sprite = GameManager.Instance.collectSprites[0];
                break;
            case GameManager.Player.Frog:
                animator.runtimeAnimatorController = GameManager.Instance.collectAnimator[1];
                spriteRenderer.sprite = GameManager.Instance.collectSprites[0];
                break;
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        rb2D.velocity = new Vector2(horizontalInput * moveSpeed,rb2D.velocity.y);
        if (Input.GetKey("space") && CheckGround.isGround && !animator.GetBool("Jump"))
        {
            animator.SetBool("Jump",true);
            animator.SetBool("Ground", false);
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }else if (CheckGround.isGround)
        {
            animator.SetBool("Ground", true);
        }

        if(rb2D.velocity.y < 0)
        {
            animator.SetBool("Jump", false);
        }


    }
}
