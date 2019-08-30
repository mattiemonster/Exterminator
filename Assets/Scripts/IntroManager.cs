using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public TextTyper textTyper;
    public GameObject textObject;
    public GameObject loadingDialogObject;
    public AudioSource continueSound;

    void Start()
    {
        if (LevelMaster.viewedIntro)
        {
            loadingDialogObject.SetActive(true);
            SceneManager.LoadSceneAsync("Level");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            continueSound.Play();
            if (!textTyper.AnyEntriesLeft)
            {
                loadingDialogObject.SetActive(true);
                LevelMaster.viewedIntro = true;
                SceneManager.LoadSceneAsync("Level");
                return;
            }

            textTyper.LoadNextString();
            textObject.GetComponent<Animator>().StopPlayback();
            textObject.GetComponent<Animator>().Play("ContinueAnim");
        }
    }
}
