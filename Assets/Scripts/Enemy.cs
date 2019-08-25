using UnityEngine;

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
}
