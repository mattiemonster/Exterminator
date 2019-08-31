using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public TextTyper textTyper;
    public GameObject textObject;
    public GameObject loadingDialogObject;
    public AudioSource continueSound;
    public string nextLevel = "Level";
    public bool isIntro = true;

    void Start()
    {
        if (LevelMaster.viewedIntro && isIntro)
        {
            loadingDialogObject.SetActive(true);
            SceneManager.LoadSceneAsync(nextLevel);
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
                SceneManager.LoadSceneAsync(nextLevel);
                return;
            }

            textTyper.LoadNextString();
            textObject.GetComponent<Animator>().StopPlayback();
            textObject.GetComponent<Animator>().Play("ContinueAnim");
        }
    }
}
