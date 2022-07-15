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

    [HideInInspector]
    public float attackDamage;                  // Entity's attack damage
    [HideInInspector]
    public float attackSpeed;                   // Entity's number of attack per second



    [Header("Mobility stats")]
    [Tooltip("Entity's base run speed")]
    public float baseRunSpeed = 60;             // Entity's base run speed

    [HideInInspector]
    public float runSpeed;                      // Entity's run speed


}
