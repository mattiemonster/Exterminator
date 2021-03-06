﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Game/Projectile")]
public class Projectile : ScriptableObject
{
    public float damage = 10f;
    public float speed = 10f;
    public bool explosive = false;
    public float explosionRange = 5f;
    public float maxFlyTime = 10f;
    public AudioClip explosionSound;
    public GameObject collideEffect;
    public Source source;
}
