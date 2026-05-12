using UnityEngine;
using System.Collections;
 
/// <summary>
/// Controls a Thwomp-style trap that drops from its starting position to a fixed
/// ground level, waits briefly, then rises back up. Kills any player it lands on.
/// </summary>
public class ThwompTrap : MonoBehaviour
{
    [Header("Movement Settings")]
    public float dropSpeed  = 10f;   // Speed while falling toward the ground
    public float riseSpeed  = 2f;    // Speed while returning to the original position
    public float waitTimeAtBottom = 1f; // Seconds to wait on the ground before rising
 
    [Header("Audio Settings")]
    public AudioSource startSound;   // Plays while the Thwomp is descending
    public AudioSource impactSound;  // Plays on impact with the ground
 
    private Vector3 originalPosition;
    private bool isAttacking = false;
 
    private void Start()
    {
        // Store the spawn position so the Thwomp can return to it after each attack
        originalPosition = transform.position;
    }
 
    /// <summary>
    /// Begins the attack sequence if the Thwomp is not already attacking.
    /// Called externally by GameManager.
    /// </summary>
    public void TriggerAttack()
    {
        if (!isAttacking)
            StartCoroutine(AttackSequence());
    }
 
    /// <summary>
    /// Coroutine that drives the three-phase attack:
    ///   1. Drop  – move quickly to ground level while playing startSound.
    ///   2. Wait  – pause at ground level and play impactSound.
    ///   3. Rise  – slowly return to originalPosition.
    /// </summary>
    private IEnumerator AttackSequence()
    {
        isAttacking = true;
 
        // ── Phase 1: Drop ────────────────────────────────────────────────────
        if (startSound != null) startSound.Play();
 
        Vector3 groundPosition = new Vector3(transform.position.x, -2.30f, transform.position.z);
 
        while (Vector3.Distance(transform.position, groundPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, groundPosition, dropSpeed * Time.deltaTime);
            yield return null;
        }
 
        // ── Phase 2: Wait at ground level ────────────────────────────────────
        if (startSound  != null) startSound.Stop();
        if (impactSound != null) impactSound.Play();
 
        yield return new WaitForSeconds(waitTimeAtBottom);
 
        if (impactSound != null) impactSound.Stop();
 
        // ── Phase 3: Rise back to original position ───────────────────────────
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }
 
        isAttacking = false;
    }
 
    /// <summary>
    /// Kills the player immediately when the Thwomp lands on them.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDeath deathScript = collision.gameObject.GetComponent<PlayerDeath>();
            if (deathScript != null)
                deathScript.Die();
        }
    }
}