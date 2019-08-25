using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentWeaponUI : MonoBehaviour
{
    [Header("Scene References")]
    public TextMeshProUGUI weaponName;
    public Image weaponIcon;

    public void SetWeapon(Weapon weapon)
    {
        weaponName.text = weapon.weaponName;
        weaponIcon.sprite = weapon.weaponIcon;
        weaponIcon.color = Color.white;
    }
}
