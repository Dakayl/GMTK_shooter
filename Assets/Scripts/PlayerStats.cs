using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// References every stat available to the player in 3 categories : Defensive, Offensive and Mobility
/// Every player stat has a "base" version that can be configured in Unity's editor, and a "current" version
/// used in-game.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; } // Singleton
    #region Variables

    [Header("Defensive stats")]
    [Tooltip("Entity's base max HP")]
    public float baseMaxHP = 100;               // Entity's base max HP
    [Tooltip("Entity's base incoming damage flat reduction")]
    public float baseArmor = 0;                 // Entity's incoming damage flat reduction
    [Tooltip("Entity's base invincibilty frame duration")]
    public float baseInvinvicilityFramesDuration = 0.75f; // Entity's base invincibility frames duration

    [HideInInspector]
    public float maxHP;                         // Entity's max HP
    [HideInInspector]
    public float currentHP;                     // Entity's current HP
    [HideInInspector]
    public float armor;                         // Entity's incoming damage reduction
    [HideInInspector]
    public float invinvicilityFramesDuration;   // Entity's invincibility frames duration
    [HideInInspector]
    public bool areInvicibilityFramesActive = false;    // Are invicibility frames active?
    [HideInInspector]
    public bool isDead = false;                 // Is the entity dead?



    [Header("Offensive stats")]
    [Tooltip("Entity's base attack damage")]
    public float baseAttackDamage = 3;          // Entity's base attack damage
    [Tooltip("Entity's base attack speed")]
    public float baseAttackSpeed = 2;           // Entity's base attack per second
     [Tooltip("Entity's base attack range")]
    public float baseAttackRange = 25;           // Entity's base range


    [HideInInspector]
    public float attackDamage;                  // Entity's current attack damage
    [HideInInspector]
    public float attackSpeed;                   // Entity's current number of attack per second
    [HideInInspector]
    public float attackRange;                   // Entity's current attack range

    private bool isPiercingMode = false;        // Is Player shooting piercing bullets ?
    public void ActivatePiercing() { isPiercingMode = true; }
    public bool isPiercingActivated { get { return isPiercingMode; }}

    private bool isBouncingMode = false;         // Is Player shooting bouncing bullets ?
    public void ActivateBouncing() { isBouncingMode = true; }
    public bool isBoucingActivated { get { return isBouncingMode; }}

    private bool isFireMode = false;             // Is Player shooting fire bullets ?
    public void ActivateFire() { isFireMode = true; }
    public bool isFireActivated { get { return isFireMode; }}
    
    private bool isPoisonMode = false;         // Is Player shooting poison bullets ?
    public void ActivatePoison() { isPoisonMode = true; }
    public bool isPoisonActivated { get { return isPiercingMode; }}

    [Header("Mobility stats")]
    [Tooltip("Entity's base run speed")]
    public float baseRunSpeed = 60;             // Entity's base run speed

    [HideInInspector]
    public float runSpeed;                      // Entity's run speed

    #endregion


    #region MonoBehavior Methods

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple PlayerStats Error");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ResetStats();
        HealToFull();

    }

    #endregion

    #region Public Functions

    /// <summary>
    /// Heal player to full
    /// </summary>
    public void HealToFull()
    {
        currentHP = maxHP;
    }

    /// <summary>
    /// Reset all player stats to their default values
    /// </summary>
    public void ResetStats()
    {
        // Setting all current values to base values
        maxHP = baseMaxHP;
        armor = baseArmor;
        invinvicilityFramesDuration = baseInvinvicilityFramesDuration;
        attackDamage = baseAttackDamage;
        attackSpeed = baseAttackSpeed;
        runSpeed = baseRunSpeed;
        isPiercingMode = false;
        isBouncingMode = false;
        isFireMode = false;
        isPoisonMode = false;
    }

    /// <summary>
    /// Activate invincinbility frames for their default duration
    /// </summary>
    public void ActivateInvincinbilityFrames()
    {
        StartCoroutine(InvincibilityFrames(invinvicilityFramesDuration));
    }

    /// <summary>
    /// Activate invincinbility frames for a custom duration
    /// </summary>
    /// <param name="customDuration"></param>
    public void ActivateInvincinbilityFrames(float customDuration)
    {
        StartCoroutine(InvincibilityFrames(customDuration));
    }

    #endregion


    #region Coroutines

    /// <summary>
    /// Sets invincibilityFrames to true, wait for duration, then set to false
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator InvincibilityFrames(float duration)
    {
        areInvicibilityFramesActive = true;

        yield return new WaitForSeconds(duration);

        areInvicibilityFramesActive = false;
    }

    #endregion
}
