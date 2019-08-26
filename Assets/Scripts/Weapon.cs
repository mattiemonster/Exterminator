using UnityEngine;

public enum AmmoType
{
    Hitscan,
    Projectile
}

public enum FireMode
{
    SingleFire,
    Auto
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class Weapon : ScriptableObject
{
    [Tooltip("Weapon name shown in the current weapon UI.")]
    public string weaponName = "New Weapon";
    public int maxReserveAmmo = 150;
    public int maxCurrentAmmo = 20;
    [Tooltip("Damage weapon does to enemies and/or player if enemy carries it.")]
    public float damage = 5;
    [Tooltip("How fast the weapon can fire. Smaller is faster.")]
    public float fireSpeed = 2f;
    [Tooltip("How far the hitscan ray can travel.")]
    public float range = 100f;
    [Tooltip("If the weapon ready UI should appear when cooldown is finished. Turn off for rapid fire.")]
    public bool showWeaponReady = true;
    [Tooltip("Type of ammo for the weapon to fire. Hitscan shoots an invisble ray and damages hit enemies, projectile fires a damaging projectile based of a prefab.")]
    public AmmoType ammoType = AmmoType.Hitscan;
    [Tooltip("If the weapon should be single fire or automatic.")]
    public FireMode fireMode = FireMode.SingleFire;
    [Tooltip("Weapon prefab used for viewmodels.")]
    public GameObject weaponPrefab;
    [Tooltip("Sprite shown in current weapon UI.")]
    public Sprite weaponIcon;
    [Tooltip("Sound played when the weapon is fired")]
    public AudioClip shootSound;
}
