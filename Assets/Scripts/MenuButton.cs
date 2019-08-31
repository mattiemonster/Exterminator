using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum ButtonType
{
    START,
    OPTIONS,
    QUIT,
    OPTIONS_CLOSE
}

public class MenuButton : MonoBehaviour
{
    [Header("Values")]
    public string buttonText;
    public ButtonType buttonType;
    public string startLevel = "IntroText";

    [Header("Asset References")]
    public AudioClip hoverSound;
    public AudioClip buttonPressedSound;

    [Header("Scene References")]
    public GameObject cover;
    public GameObject optionsDialog;

    private TextMeshProUGUI text;
    private AudioSource audioSrc;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        audioSrc = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        audioSrc.clip = hoverSound;
        audioSrc.Play();
        text.text = "> " + buttonText;
        text.fontStyle = FontStyles.Italic;
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        text.text = buttonText;
        text.fontStyle = FontStyles.Normal;
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.START:
                cover.SetActive(true);
                cover.GetComponent<Animator>().Play("ShowCover");
                StartCoroutine(StartGame());
                break;
            case ButtonType.OPTIONS:
                optionsDialog.SetActive(true);
                break;
            case ButtonType.QUIT:
                cover.SetActive(true);
                cover.GetComponent<Animator>().Play("ShowCover");
                Debug.Log("Quitting game, main menu button pressed.");
                StartCoroutine(QuitGame());
                break;
            case ButtonType.OPTIONS_CLOSE:
                optionsDialog.SetActive(false);
                OnPointerExit(eventData);
                break;

            default:
                return;
        }
        audioSrc.clip = buttonPressedSound;
        if (!(buttonType == ButtonType.OPTIONS_CLOSE)) audioSrc.Play();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(buttonPressedSound.length + 0.05f);
        SceneManager.LoadScene(startLevel);
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(buttonPressedSound.length + 0.05f);
        Application.Quit();
    }
}
