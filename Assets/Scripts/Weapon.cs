using UnityEngine;

public enum AmmoType
{
    Hitscan,
    Projectile
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName = "New Weapon";
    public int maxReserveAmmo = 150;
    public int maxCurrentAmmo = 20;
    public float damage = 5;
    public AmmoType ammoType = AmmoType.Hitscan;
    public GameObject weaponPrefab;
    public Sprite weaponIcon;
}
