using UnityEngine;
 
/// <summary>
/// Detects whether the player is currently standing on the ground
/// using a trigger collider placed at the player's feet.
/// </summary>
public class CheckGround : MonoBehaviour
{
    // Shared ground state accessible by other scripts (e.g. PlayerMove)
    public static bool isGrounded;
 
    /// <summary>
    /// Called every frame while another collider stays inside this trigger.
    /// As long as the feet trigger overlaps ground, the player is grounded.
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }
 
    /// <summary>
    /// Called when a collider leaves this trigger.
    /// The player is no longer grounded once the feet leave the surface.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
 