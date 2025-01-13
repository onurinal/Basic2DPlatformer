using UnityEngine;

namespace Basic2DPlatformer.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Rigidbody2D myRigidbody2D;

        // jump
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private PlayerJumpState playerJumpState;
        [SerializeField] private LayerMask groundLayer;

        // climb
        private bool climbCheck;


        private void Start()
        {
        }

        private void Update()
        {
            playerInput.UpdateInput();
            GroundCheck();
            Move();
            CanJump();
            Climb();
        }

        private void Move()
        {
            var horizontalMovementSpeed = playerInput.HorizontalInput * (playerProperties.horizontalMovementSpeed * Time.deltaTime);
            transform.Translate(horizontalMovementSpeed, 0f, 0f);

            // change the direction player's face
            var horizontalMovement = Mathf.Abs(playerInput.HorizontalInput) > Mathf.Epsilon;
            if (horizontalMovement)
            {
                transform.localScale = new Vector2(Mathf.Sign(playerInput.HorizontalInput), transform.localScale.y);
            }
        }

        private void GroundCheck()
        {
            if (groundChecker.IsGrounded && myRigidbody2D.velocity.y <= 0)
            {
                playerJumpState = PlayerJumpState.None;
            }
        }

        //-------------------------- JUMP --------------------------
        private void CanJump()
        {
            if (climbCheck)
            {
                return;
            }

            if (playerJumpState == PlayerJumpState.None && playerInput.JumpKey)
            {
                FirstJump();
                return;
            }

            if (playerJumpState == PlayerJumpState.FirstJump && playerInput.JumpKey)
            {
                SecondJump();
            }
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

        // -------------------------- CLIMB --------------------------
        public void LadderStateChange(bool entered)
        {
            if (entered)
            {
                climbCheck = true;
                myRigidbody2D.gravityScale = 0f;
                myRigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                climbCheck = false;
                myRigidbody2D.gravityScale = playerProperties.gravityScale;
            }
        }

        private void Climb()
        {
            if (Mathf.Abs(playerInput.VerticalInput) > Mathf.Epsilon && climbCheck)
            {
                var verticalMovementSpeed = playerInput.VerticalInput * (playerProperties.verticalMovementSpeed * Time.deltaTime);
                transform.Translate(0f, verticalMovementSpeed, 0f);
            }
        }
    }
}