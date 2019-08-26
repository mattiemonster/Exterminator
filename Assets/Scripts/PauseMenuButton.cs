using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum PauseButtonType
{
    RESUME,
    MAIN,
    QUIT
}

public class PauseMenuButton : MonoBehaviour
{
    [Header("Values")]
    public string buttonText;
    public PauseButtonType buttonType;

    [Header("Asset References")]
    public AudioClip hoverSound;

    [Header("Scene References")]
    public GameObject pauseMenu;
    public PauseMenuManager pauseMenuManager;
    public PlayerCamera playerCam;

    private TextMeshProUGUI text;
    private AudioSource audioSrc;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        audioSrc = GetComponent<AudioSource>();
    }

    void Awake()
    {
        if (!text) return;
        text.text = buttonText;
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        if (!pauseMenuManager.allowClose) return;

        audioSrc.clip = hoverSound;
        audioSrc.Play();
        text.text = "> " + buttonText;
        text.fontStyle = FontStyles.Italic;
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        if (!pauseMenuManager.allowClose) return;

        text.text = buttonText;
        text.fontStyle = FontStyles.Normal;
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        if (!pauseMenuManager.allowClose) return;

        switch (buttonType)
        {
            case PauseButtonType.RESUME:
                pauseMenu.GetComponent<Animator>().Play("PauseMenuClose");
                StartCoroutine(pauseMenuManager.EnableInput());
                break;
            case PauseButtonType.MAIN:
                SceneManager.LoadScene("Menu");
                break;
            case PauseButtonType.QUIT:
                Debug.Log("Quitting game, pause menu button pressed.");
                Application.Quit();
                break;

            default:
                return;
        }
    }

    IEnumerator CloseMenu()
    {
        yield return new WaitForSeconds(20 / 60 + 0.05f);
        pauseMenu.SetActive(false);
    }

}
