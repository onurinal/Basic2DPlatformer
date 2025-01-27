using UnityEngine;

namespace Basic2DPlatformer.Player
{
    [CreateAssetMenu(fileName = "Player Properties", menuName = "Basic 2D Platformer/Create Player Properties")]
    public class PlayerProperties : ScriptableObject
    {
        public float horizontalMovementSpeed = 5f;
        public float verticalMovementSpeed = 1f;
        public float jumpForce = 8f;

        public float gravityScale = 3f;
    }
}