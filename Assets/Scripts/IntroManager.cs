using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public TextTyper textTyper;
    public GameObject textObject;
    public GameObject loadingDialogObject;
    public GameObject cover;
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
                if (isIntro)
                {
                    loadingDialogObject.SetActive(true);
                    LevelMaster.viewedIntro = true;
                    SceneManager.LoadSceneAsync(nextLevel);
                } else
                {
                    cover.SetActive(true);
                    StartCoroutine(TransitionDelayed());
                }
                return;
            }

            textTyper.LoadNextString();
            textObject.GetComponent<Animator>().StopPlayback();
            textObject.GetComponent<Animator>().Play("ContinueAnim");
        }
    }

    IEnumerator TransitionDelayed(float time = 0.75f)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(nextLevel);
    }
}
