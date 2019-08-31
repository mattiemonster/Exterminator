using UnityEngine;

public enum EnemyAttackType
{
    Melee,
    Projectile
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Game/Enemy")]
public class Enemy : ScriptableObject
{
    [Tooltip("Enemy name")]
    public string enemyName = "Enemy";
    [Tooltip("Max and starting health of the enemy")]
    public float maxHealth = 10f;
    [Tooltip("Damage enemy does to player with its melee or ranged attack")]
    public float damage = 5f;
    [Tooltip("Speed at which enemy moves to player")]
    public float movementSpeed = 12f;
    [Tooltip("Time it takes for an enemies attack cooldown to wear off.")]
    public float attackDelay = 1.5f;
    [Tooltip("Knockback force applied to enemy when it attacks.")]
    public float attackKnockback = 40f;
    [Tooltip("If true, basic enemy ai (move directly to player position) will be disabled")]
    public bool supressBasicAI = false;
    public bool isBossEnemy = false;
    [Tooltip("Sound played when enemy is hurt.")]
    public AudioClip hurtSound;
    [Tooltip("Enemy attack type")]
    public EnemyAttackType attackType;
    [Tooltip("Attack projectile (if applicable)")]
    public GameObject projectile;
    [Tooltip("Sound played when enemy dies.")]
    public AudioClip deathSound;
}
