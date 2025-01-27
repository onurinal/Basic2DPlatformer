using UnityEngine;

namespace Basic2DPlatformer.Player
{
    public class GroundChecker : MonoBehaviour
    {
        public bool IsGrounded { get; private set; }
        private void OnTriggerEnter2D(Collider2D other)
        {
            IsGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsGrounded = false;
        }
    }
}