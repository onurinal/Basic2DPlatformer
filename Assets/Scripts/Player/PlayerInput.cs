using UnityEngine;

namespace Basic2DPlatformer.Player
{
    [CreateAssetMenu(fileName = "Player Input", menuName = "Basic 2D Platformer/Create Player Input")]
    public class PlayerInput : ScriptableObject
    {
        // Movement inputs
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }
        public bool JumpKey { get; private set; }

        public void UpdateInput()
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");

            JumpKey = Input.GetButtonDown("Jump");
        }
    }
}