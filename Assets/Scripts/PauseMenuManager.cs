using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject pauseMenu;
    public PlayerCamera playerCam;

    [HideInInspector]
    public bool allowClose = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.GetComponent<Animator>().Play("PauseMenuClose");
                StartCoroutine(EnableInput());
            }
            else
            {
                allowClose = false;
                playerCam.MouseLock(false);
                pauseMenu.SetActive(true);
                pauseMenu.GetComponent<Animator>().Play("PauseMenuOpen");
                StartCoroutine(AllowClose());
            }
        }
    }

    IEnumerator AllowClose()
    {
        yield return new WaitForSeconds(0.67f);
        allowClose = true;
    }

    public IEnumerator EnableInput(float delay = 0.38f)
    {
        yield return new WaitForSeconds(delay);
        playerCam.MouseLock(true);
        pauseMenu.SetActive(false);
        allowClose = false;
    }
}
