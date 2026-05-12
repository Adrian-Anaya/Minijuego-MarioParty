using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
 
/// <summary>
/// Central game controller (Singleton). Manages the countdown timer,
/// triggers random Thwomp attacks on an interval, tracks player deaths,
/// and ends or restarts the game when the win/lose conditions are met.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton reference — other scripts access the manager through this
    public static GameManager instance;
 
    [Header("Timer Settings")]
    public float gameTime = 60f;        // Seconds players must survive to win
 
    [Header("Thwomp Settings")]
    public List<ThwompTrap> enemies;    // All ThwompTrap objects in the scene
    public float attackInterval = 2f;  // Seconds between random Thwomp attacks
 
    [Header("Player Settings")]
    public List<PlayerDeath> players;  // All PlayerDeath components in the scene
 
    private bool gameOver = false;
 
    // ── Unity lifecycle ───────────────────────────────────────────────────────
 
    private void Awake()
    {
        // Enforce a single GameManager instance; destroy duplicates
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    private void Start()
    {
        // Begin triggering random Thwomp attacks after a 1-second delay
        InvokeRepeating(nameof(TriggerRandomAttack), 1f, attackInterval);
    }
 
    private void Update()
    {
        if (gameOver) return;
 
        if (gameTime > 0f)
        {
            // Count down the survival timer each frame
            gameTime -= Time.deltaTime;
        }
        else
        {
            // Timer reached zero — players survived long enough to win
            EndGame("Time's up! You survived!");
        }
    }
 
    // ── Game logic ────────────────────────────────────────────────────────────
 
    /// <summary>
    /// Picks a random Thwomp from the list and triggers its attack,
    /// provided the game has not already ended.
    /// </summary>
    private void TriggerRandomAttack()
    {
        if (gameOver || enemies.Count == 0) return;
 
        int index = Random.Range(0, enemies.Count);
 
        if (enemies[index] != null)
            enemies[index].TriggerAttack();
    }
 
    /// <summary>
    /// Called by PlayerDeath whenever a player dies.
    /// Counts remaining survivors and ends the game if none are left.
    /// </summary>
    public void RegisterDeath()
    {
        int survivors = 0;
 
        foreach (PlayerDeath player in players)
        {
            if (player != null && !player.IsDead())
                survivors++;
        }
 
        Debug.Log("Remaining players: " + survivors);
 
        if (survivors <= 0)
            EndGame("All players have died! GAME OVER");
    }
 
    /// <summary>
    /// Stops all attacks, logs the outcome, and schedules a scene reload.
    /// </summary>
    /// <param name="message">Message describing the end-game condition.</param>
    private void EndGame(string message)
    {
        gameOver = true;
        CancelInvoke(nameof(TriggerRandomAttack));
        Debug.Log(message);
 
        // Wait 3 seconds before reloading so players can read the result
        Invoke(nameof(ReloadScene), 3f);
    }
 
    /// <summary>
    /// Reloads the active scene to restart the game.
    /// </summary>
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}