using Basic2DPlatformer.Player;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private CompositeCollider2D ladderCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.LadderStateChange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.LadderStateChange(false);
        }
    }
}