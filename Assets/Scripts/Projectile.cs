using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Game/Projectile")]
public class Projectile : ScriptableObject
{
    public float damage = 10f;
    public float speed = 10f;
    public GameObject collideEffect;
    public Source source;
}
