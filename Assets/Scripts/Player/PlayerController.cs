using UnityEngine;

namespace Basic2DPlatformer.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Rigidbody2D myRigidbody2D;

        // jump
        [SerializeField] private PlayerJumpState playerJumpState;
        [SerializeField] private LayerMask groundLayer;
        private bool isDoubleJumping = false;
        private bool isGrounded = false;


        private void Start()
        {
        }

        private void Update()
        {
            playerInput.UpdateInput();
            GroundCheck();
            Move();
            CanJump();
        }

        private void Move()
        {
            var horizontalMovement = playerInput.HorizontalInput * (playerProperties.movementSpeed * Time.deltaTime);
            transform.Translate(horizontalMovement, 0f, 0f);
        }

        private void GroundCheck()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer);

            if (isGrounded && myRigidbody2D.velocity.y <= 0)
            {
                playerJumpState = PlayerJumpState.None;
            }
        }

        //-------------------------- JUMP --------------------------
        private void CanJump()
        {
            if (playerJumpState == PlayerJumpState.None && playerInput.JumpKey)
            {
                FirstJump();
                return;
            }

            if (playerJumpState == PlayerJumpState.FirstJump && playerInput.JumpKey)
            {
                SecondJump();
                return;
            }

            // if (playerJumpState == PlayerJumpState.SecondJump && myRigidbody2D.velocity.y <= 0 && isGrounded)
            // {
            //     playerJumpState = PlayerJumpState.FirstJump;
            //     isDoubleJumping = false;
            // }
        }

        private void FirstJump()
        {
            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D.AddForce(new Vector2(0f, playerProperties.jumpForce), ForceMode2D.Impulse);
            playerJumpState = PlayerJumpState.FirstJump;
        }

        private void SecondJump()
        {
            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D.AddForce(new Vector2(0f, playerProperties.jumpForce), ForceMode2D.Impulse);
            playerJumpState = PlayerJumpState.SecondJump;
        }
    }
}