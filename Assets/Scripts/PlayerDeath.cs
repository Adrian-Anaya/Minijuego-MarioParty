using UnityEngine;
 
/// <summary>
/// Manages the player's death state: disables movement, freezes the Rigidbody,
/// triggers the death animation, plays the death sound, and notifies the GameManager.
/// Attach this component to each player GameObject.
/// </summary>
public class PlayerDeath : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource deathSound;
 
    private Animator animator;
    private PlayerMove movementScript;
    private bool isDead = false;
 
    private void Start()
    {
        animator       = GetComponent<Animator>();
        movementScript = GetComponent<PlayerMove>();
    }
 
    /// <summary>
    /// Triggers the player's death sequence.
    /// Calling this more than once has no effect (guarded by isDead).
    /// </summary>
    public void Die()
    {
        if (isDead) return;
 
        isDead = true;
        Debug.Log(gameObject.name + " has died.");
 
        // Disable movement so the player can no longer be controlled
        if (movementScript != null)
            movementScript.enabled = false;
 
        // Freeze the physics body to stop all movement
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
 
        // Play the death animation
        if (animator != null)
            animator.SetTrigger("Die");
 
        // Stop footstep audio if it was playing
        if (movementScript != null && movementScript.footstepsSound != null)
            movementScript.footstepsSound.Stop();
 
        // Play the death sound effect
        if (deathSound != null)
            deathSound.Play();
 
        // Notify the GameManager so it can check remaining survivors
        GameManager.instance.RegisterDeath();
    }
 
    /// <returns>True if this player has already died.</returns>
    public bool IsDead()
    {
        return isDead;
    }
}