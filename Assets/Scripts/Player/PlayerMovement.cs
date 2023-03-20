using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForse;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float WallJumpCooldown;
    private float InputHorizontal;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();    
    }

    private void Update()
    {
        bool isOnWall = onWall();
        InputHorizontal = Input.GetAxis("Horizontal");

        

        anim.SetBool("run", InputHorizontal != 0);
        anim.SetBool("grounded", isGrounded());

        if(WallJumpCooldown < 0.2f)
        {
            if (!isOnWall)
            {
                body.velocity = new Vector2(InputHorizontal * moveSpeed, body.velocity.y);
                if (InputHorizontal > 0.01f)
                    transform.localScale = Vector3.one;
                else if (InputHorizontal < -0.01f)
                    transform.localScale = new Vector3(-1, 1, 1);
            }

            if(isOnWall && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 5;
            }


            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }
        else
        {
            WallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpForse);
            anim.SetTrigger("jump");
            AudioManager.instanse.PlayAudio(jumpSound);
        }else if(onWall() && !isGrounded())
        {
            if (InputHorizontal == 0) 
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            WallJumpCooldown = 0;
            
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return InputHorizontal == 0 && isGrounded() && !onWall();
    }
}
