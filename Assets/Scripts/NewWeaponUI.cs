using System.Collections;
using UnityEngine;

public class NewWeaponUI : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenUI()
    {
        animator.Play("WeaponObtainedAnim");
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
