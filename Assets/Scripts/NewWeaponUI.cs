using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewWeaponUI : MonoBehaviour
{
    Animator animator;

    [Header("Scene References")]
    public TextMeshProUGUI text;
    public Image icon;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenUI(Weapon weapon)
    {
        animator.Play("WeaponObtainedAnim");
        text.text = weapon.weaponName;
        icon.sprite = weapon.weaponIcon;
        StartCoroutine(CloseUIDelay());
    }

    public void CloseUI()
    {
        animator.Play("WeaponObtainedCloseAnim");
    }

    public IEnumerator CloseUIDelay()
    {
        yield return new WaitForSeconds(2f);
        CloseUI();
    }
}
